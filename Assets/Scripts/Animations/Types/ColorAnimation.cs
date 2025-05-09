using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Animations.Types
{
    public class ColorAnimation : AnimationObject
    {
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;
        
        public override void SetToStartValue() { }

        public override void Animate(Transform animateObject)
            => AnimateRequiredObject(animateObject);
        
        private void AnimateRequiredObject(Transform animateObject)
        {
            if (animateObject.TryGetComponent(out Image image))
            {
                image.color = startColor;
                image.DOColor(endColor, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => {OnAnimationEnd?.Invoke();});
            }
            
            if (animateObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.color = startColor;
                spriteRenderer.DOColor(endColor, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => {OnAnimationEnd?.Invoke();});
            }
        }
    }
}