using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Util.Tweening
{
    public abstract partial class Tween
    {
        public class SequentialTween : BaseTween, ITween
        {
            private Action callback;

            public static SequentialTween Create(
                GameObject parent,
                bool destroyOnFinishTween = false
            )
            {
                SequentialTween sequentialTween = parent.AddComponent<SequentialTween>();
                sequentialTween.destroyOnFinishTween = destroyOnFinishTween;
                return sequentialTween;
            }

            public override void StartTween(Action callback = null)
            {
                this.callback = callback;
                DequeueNextTween();
            }

            private void DequeueNextTween()
            {
                if (!TweenQueue.Any())
                {
                    callback?.Invoke();
                    CleanUp();
                }
                else
                {
                    Func<IEnumerator> nextRoutine = TweenQueue.Dequeue();
                    StartCoroutine(nextRoutine());
                }
            }

            protected override void TweenDoneCoroutine()
            {
                DequeueNextTween();
            }
        }
    }
}