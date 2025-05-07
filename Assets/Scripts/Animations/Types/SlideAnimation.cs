using DG.Tweening;
using UnityEngine;

namespace Animations.Types
{
    public class SlideAnimation : AnimationObject
    {
        [SerializeField] Transform _startPosition;
        [SerializeField] Transform _endPosition;

        public override void Animate()
        {
            SetToStartValue();
            transform.DOMove(_endPosition.position, _duration).SetLoops(_loopNuber, _loopType).SetEase(_ease).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }

        public override void Animate(Transform animateObject)
        {
            animateObject.position = _startPosition.position;
            animateObject.DOMove(_endPosition.position, _duration).SetLoops(_loopNuber, _loopType).SetEase(_ease).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }

        public override void SetToStartValue()
        {
            transform.position = _startPosition.position;
        }

        public override void AnimateReverce()
        {
            transform.position = _endPosition.position;
            transform.DOMove(_startPosition.position, _duration).SetLoops(_loopNuber, _loopType).SetEase(_ease).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }

        public void MoveToPoint(Transform targetPosition)
        {
            transform.DOLocalMove(targetPosition.position, _duration).SetEase(_ease).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }
    }
}
