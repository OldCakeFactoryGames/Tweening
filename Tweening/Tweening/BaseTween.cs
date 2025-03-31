using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.Tweening
{
    public abstract class BaseTween : MonoBehaviour, ITween
    {
        protected readonly Queue<Func<IEnumerator>> TweenQueue = new();
        protected bool destroyOnFinishTween;

        protected abstract void TweenDoneCoroutine();

        protected void QueueGenericTween<T>(
            Func<T> getFloat,
            Action<T> setFloat,
            Func<T, T, float, T> lerp,
            T endValue,
            float timeToTween,
            Func<float, float> easingFunction = null
        )
        {
            easingFunction ??= EasingFunctions.Linear;

            IEnumerator TweenFunctionToQueue()
            {
                yield return StartCoroutine(
                    TweenGenericCoroutine(getFloat, setFloat, lerp, endValue, timeToTween,
                        easingFunction
                    ));
            }

            TweenQueue.Enqueue(TweenFunctionToQueue);
        }

        private IEnumerator TweenGenericCoroutine<T>(
            Func<T> getValue,
            Action<T> setValue,
            Func<T, T, float, T> lerp,
            T endValue,
            float t,
            Func<float, float> easingFunction
        )
        {
            var elapsedTime = 0f;
            T startValue = getValue();

            while (elapsedTime < t)
            {
                var f = elapsedTime / t;
                T currentValue = lerp(startValue, endValue, easingFunction(f));
                setValue(currentValue);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            setValue(endValue);
            TweenDoneCoroutine();
        }

        public abstract void StartTween(Action callback = null);

        public ITween Stitch(ITween other)
        {
            TweenQueue.Enqueue(StartOther);
            return this;

            IEnumerator StartOther()
            {
                other.StartTween(TweenDoneCoroutine);
                yield return null;
            }
        }

        public ITween GenericTween<T>(
            Func<T> getValue,
            Action<T> setValue,
            Func<T, T, float, T> lerp,
            T endValue,
            float timeToTween,
            Func<float, float> easingFunction = null
        )
        {
            QueueGenericTween(getValue, setValue, lerp, endValue, timeToTween, easingFunction);
            return this;
        }

        public ITween Scale(
            GameObject toTween,
            Vector3 endScale,
            float timeToTween,
            Func<float, float> easingFunction = null
        )
        {
            QueueGenericTween(
                () => toTween.transform.localScale,
                newScale => toTween.transform.localScale = newScale,
                Vector3.LerpUnclamped,
                endScale,
                timeToTween,
                easingFunction
            );

            return this;
        }

        public ITween Rotate(
            GameObject toTween,
            Vector3 endRotation,
            float timeToTween,
            Func<float, float> easingFunction = null
        )
        {
            QueueGenericTween(
                () => toTween.transform.localRotation,
                newRotation => toTween.transform.localRotation = newRotation,
                Quaternion.LerpUnclamped,
                Quaternion.Euler(endRotation),
                timeToTween,
                easingFunction
            );

            return this;
        }

        public ITween Move(
            GameObject toTween,
            Vector3 endPosition,
            float timeToTween,
            Func<float, float> easingFunction = null
        )
        {
            if (toTween.transform is RectTransform rectTransform)
            {
                Vector2 anchoredPosition = rectTransform.anchoredPosition;

                var intermediateVector = new Vector3(
                    anchoredPosition.x, 
                    anchoredPosition.y,
                    rectTransform.position.z);

                QueueGenericTween(
                    () => intermediateVector,
                    interpolated =>
                    {
                        rectTransform.anchoredPosition = interpolated;
                        
                        Vector3 vector3 = rectTransform.position;
                        vector3.z = interpolated.z;
                        rectTransform.position = vector3;
                    },
                    Vector3.LerpUnclamped,
                    endPosition,
                    timeToTween,
                    easingFunction
                );
            }
            else
            {
                QueueGenericTween(
                    () => toTween.transform.localPosition,
                    newScale => toTween.transform.localPosition = newScale,
                    Vector3.LerpUnclamped,
                    endPosition,
                    timeToTween,
                    easingFunction
                );
            }

            return this;
        }


        public ITween Wait(float time)
        {
            TweenQueue.Enqueue(WaitCoroutine);

            return this;

            IEnumerator WaitCoroutine()
            {
                yield return new WaitForSeconds(time);
                TweenDoneCoroutine();
            }
        }

        public ITween Do(Action todo)
        {
            TweenQueue.Enqueue(DoCoroutine);

            return this;

            IEnumerator DoCoroutine()
            {
                todo?.Invoke();
                TweenDoneCoroutine();
                yield break;
            }
        }

        public void CleanUp()
        {
            if (destroyOnFinishTween)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}