using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Animations;
using Animations.Types;
using UnityEngine;
using Zenject;

namespace Game.Figure
{
    public class PathAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationController pathAnimation;
        private GameConfig _config;
        
        CancellationTokenSource _tokenSource;
        CancellationToken _token;
        
        [Inject]
        public void Construct(GameConfig config)
        {
            _config = config;
            
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
        }
        
        public async void PlayAnimation(List<SpriteRenderer> objects, Action onAnimationFinished)
        {
            Callback(objects, onAnimationFinished);

            foreach (SpriteRenderer @object in objects)
            {
                if (_token.IsCancellationRequested) return;
                
                pathAnimation.StartAnimation(@object.transform);
                await Task.Delay((int)(_config.delayToShowObjects * 1000));
            }
        }

        private void Callback(List<SpriteRenderer> objects, Action onAnimationFinished)
        {
            int finishedAnimations = 0;
            pathAnimation.Callback(() =>
            {
                finishedAnimations++;
                if (finishedAnimations >= objects.Count)
                {
                    onAnimationFinished?.Invoke();
                    pathAnimation.ClearCallback();
                }
            });
        }

        private void OnDestroy()
        {
            if (_token.CanBeCanceled)
            {
                _tokenSource.Cancel();
            }
            
            _tokenSource.Dispose(); 
        }
    }
}