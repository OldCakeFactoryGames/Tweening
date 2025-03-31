using System;
using UnityEngine;

namespace Util.Tweening
{
    public interface ITween
    {
        public void StartTween(Action callback = null);

        public ITween Stitch(ITween other);

        public ITween GenericTween<T>(
            Func<T> getValue,
            Action<T> setValue,
            Func<T, T, float, T> lerp,
            T endValue,
            float timeToTween,
            Func<float, float> easingFunction = null
        );

        public ITween Scale(
            GameObject toTween,
            Vector3 endScale,
            float timeToTween,
            Func<float, float> easingFunction = null
        );

        public ITween Rotate(
            GameObject toTween,
            Vector3 endRotation,
            float timeToTween,
            Func<float, float> easingFunction = null
        );

        public ITween Move(
            GameObject toTween,
            Vector3 endPosition,
            float timeToTween,
            Func<float, float> easingFunction = null
        );

        public ITween Wait(float time);

        public ITween Do(Action todo);
        public void CleanUp();
    }
}