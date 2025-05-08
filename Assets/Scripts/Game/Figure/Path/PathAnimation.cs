using System.Collections.Generic;
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

        [Inject]
        public void Construct(GameConfig config)
        {
            _config = config;
        }
        
        public async void PlayAnimation(List<SpriteRenderer> objects)
        {
            foreach (SpriteRenderer @object in objects)
            {
                pathAnimation.StartAnimation(@object.transform);
                await Task.Delay((int)(_config.delayToShowObjects * 1000));
            }
        }
    }
}