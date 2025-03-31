using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util.Tweening
{
    public abstract partial class Tween
    {
        public class ParallelTween : BaseTween, ITween
        {
            private Action callback;
       

            public static ParallelTween Create(
                GameObject parent,
                bool destroyOnFinishTween = false
            )
            {
                ParallelTween sequentialTween = parent.AddComponent<ParallelTween>();
                sequentialTween.destroyOnFinishTween = destroyOnFinishTween;
                return sequentialTween;
            }

            public override void StartTween(Action callback = null)
            {
                this.callback = callback;
                StartCoroutine(StartAllTweenInParallel());
            }

            protected override void TweenDoneCoroutine()
            {
                //parallel tween does nothing after individual tween is done
            }

            private IEnumerator StartAllTweenInParallel()
            {
                var activeRoutines = new List<Coroutine>();

                while (TweenQueue.Any())
                {
                    Func<IEnumerator> nexTween = TweenQueue.Dequeue();
                    Coroutine routine = StartCoroutine(nexTween());
                    activeRoutines.Add(routine);
                }

                foreach (Coroutine routine in activeRoutines)
                {
                    yield return routine;
                }

                callback?.Invoke();
                CleanUp();
            }
        }
    }
}