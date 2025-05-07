using DG.Tweening;
using UnityEngine;

namespace Animations.Types
{
    public class ScaleAnimation : AnimationObject
    {
        [SerializeField] Vector3 _startScale;
        [SerializeField] Vector3 _endScale;

        public override void Animate()
        {
            SetToStartValue();
            transform.DOScale(_endScale, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }
        public override void Animate(Transform animateObject)
        {
            animateObject.localScale = _startScale;
            animateObject.DOScale(_endScale, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }

        public override void SetToStartValue()
        {
            transform.localScale = _startScale;
        }

        public override void AnimateReverce()
        {
            transform.localScale = _endScale;
            transform.DOScale(_startScale, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }

    }
}
