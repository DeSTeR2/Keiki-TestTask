using DG.Tweening;
using UnityEngine;

namespace Animations.Types
{
    public class RotationAnimation : AnimationObject
    {
        [SerializeField] Vector3 _startRotation;
        [SerializeField] Vector3 _endRotation;
        [SerializeField] RotateMode _rotateMode;

        public override void Animate()
        {
            SetToStartValue();
            transform.DORotate(_endRotation, _duration, _rotateMode).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }
        public override void Animate(Transform animateObject)
        {
            animateObject.rotation = Quaternion.Euler(_startRotation);
            animateObject.DORotate(_endRotation, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }

        public override void SetToStartValue()
        {
            transform.rotation = Quaternion.Euler(_startRotation);
        }

        public override void AnimateReverce()
        {
            transform.rotation = Quaternion.Euler(_endRotation);
            transform.DORotate(_startRotation, _duration, _rotateMode).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }
    }
}
