using System;
using System.Threading.Tasks;
using Animations.Types;
using UnityEngine;

namespace Animations
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] AnimationObject[] _animations;
        [SerializeField] bool playOnAwake = false;

        int _animationEnd = 0;

        public delegate void CallBack();

        CallBack _callBack;

        private void Start()
        {
            for (int i = 0; i < _animations.Length; i++) {
                _animations[i].OnAnimationEnd += ControlAnimationFlow;
            }

            if (playOnAwake) StartAnimation();
        }

        public AnimationController StartAnimation()
        {
            _animationEnd = 0;
            for (int i = 0; i < _animations.Length; ++i) {
                _animations[i].Animate();
            }

            return this;
        }

        public async Task<AnimationController> StartAnimationAsync()
        {
            await Task.Run(() =>
            {
                _animationEnd = 0;
                for (int i = 0; i < _animations.Length; ++i)
                {
                    _animations[i].Animate();
                }
            });

            return this;
        }

        public AnimationController StartReverceAnimation()
        {
            _animationEnd = 0;
            for (int i = 0; i < _animations.Length; ++i)
            {
                _animations[i].AnimateReverce();
            }

            return this;
        }

        public async Task<AnimationController> StartReverceAnimationAsync()
        {
            await Task.Run(() =>
            {
                _animationEnd = 0;
                for (int i = 0; i < _animations.Length; ++i)
                {
                    _animations[i].Animate();
                }
            });

            return this;
        }

        public AnimationController StartAnimation(Transform animateObject) {
            _animationEnd = 0;
            for (int i = 0; i < _animations.Length; ++i)
            {
                _animations[i].Animate(animateObject);
            }

            return this;
        }
        
        public AnimationController Callback(CallBack callBack)
        {
            _callBack = callBack;
            return this;
        }

        public void ClearCallback() => _callBack = null;

        public void SetToStartValue()
        {
            foreach (var animationObject in _animations)
            {
                animationObject.SetToStartValue();
            }
        }

        public bool IsThereInfiniteLoop()
        {
            foreach (var animation in _animations)
            {
                if (animation.IsInfinite()) return true;
            }
            return false;
        }

        private void ControlAnimationFlow()
        {
            _animationEnd++;

            if (_animationEnd >= _animations.Length) {
                _callBack?.Invoke();
                _animationEnd -= _animations.Length;
            }
        }
    }
}
