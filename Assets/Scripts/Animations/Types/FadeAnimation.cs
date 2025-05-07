using DG.Tweening;
using UnityEngine;

namespace Animations.Types
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeAnimation : AnimationObject
    {
        [SerializeField] float _startAlpha = 0;
        [SerializeField] float _endAlpha = 1;

        CanvasGroup _canvasGroup;

        private void GetCanvasGroup()
        {
            if (!_canvasGroup)
                _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void Animate()
        {
            GetCanvasGroup();

            SetToStartValue();
            _canvasGroup.DOFade(_endAlpha, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }

        public override void AnimateReverce()
        {
            GetCanvasGroup();

            _canvasGroup.alpha = _endAlpha;
            _canvasGroup.DOFade(_startAlpha, _duration).SetEase(_ease).SetLoops(_loopNuber, _loopType).OnComplete(() => { OnAnimationEnd?.Invoke(); });
        }

        public override void SetToStartValue()
        {
            _canvasGroup.alpha = _startAlpha;
        }
    }
}
