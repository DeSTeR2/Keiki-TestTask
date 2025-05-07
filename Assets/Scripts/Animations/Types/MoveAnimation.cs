using DG.Tweening;
using UnityEngine;

namespace Animations.Types
{
    public class MoveAnimation : AnimationObject
    {
        [SerializeField] Vector3 moveVector;

        private Vector3 _startPosition;
        
        public override void Animate()
        {
            if (_startPosition == null || _startPosition == Vector3.zero)
            {
                _startPosition = transform.localPosition;
            }
            
            Vector3 targetPosition = transform.localPosition + moveVector;
            transform.DOLocalMove(targetPosition, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => OnAnimationEnd?.Invoke());
        }

        public override void AnimateReverce()
        {
            Vector3 targetPosition = transform.localPosition - moveVector;
            transform.DOLocalMove(targetPosition, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => OnAnimationEnd?.Invoke());
        }

        public override void SetToStartValue()
        {
            transform.localPosition = _startPosition;
        }
    }
}