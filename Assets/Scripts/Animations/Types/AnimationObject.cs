using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Animations.Types
{
    public abstract class AnimationObject : MonoBehaviour
    {
        [SerializeField] public float _duration;
        [SerializeField] protected int _loopNuber = 0;
        [SerializeField] protected LoopType _loopType;
        [SerializeField] protected Ease _ease = Ease.Linear;

        public Action OnAnimationEnd;
        public virtual void Animate() { }
        public virtual async Task AnimateAsync() {
            await Task.Run(() => Animate());
        }

        public virtual void AnimateReverce() { }
        public virtual async Task AnimateReverceAsync()
        {
            await Task.Run(() => AnimateReverce());
        }
        public virtual void Animate(Transform animateObject) { }

        public abstract void SetToStartValue();

        internal bool IsInfinite() => _loopNuber == -1;
    }
}
