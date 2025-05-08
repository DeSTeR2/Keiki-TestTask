using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Game;
using Infrastructure.Services.Assets;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Splines;
using UnityEngine.U2D;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Figure
{
    public class SegmentPath : MonoBehaviour
    {
        [SerializeField] private Sprite circleSprite;
        [SerializeField] private Sprite starSprite;

        ObjectPool<SpriteRenderer> spriteRendererPool;
        private IAssetProviderService _assetProviderService;
        private PathAnimation _animation;
        private GameConfig _config;

        List<SpriteRenderer> activeSpriteRenderers = new List<SpriteRenderer>();

        [Inject]
        public void Construct(IAssetProviderService assetProviderService, PathAnimation animation, GameConfig config)
        {
            _config = config;
            _animation = animation;
            _assetProviderService = assetProviderService;
        }

        public async void CreatePath(SplineContainer spline, int sortOrder)
        {
            ReleaseAllRenderers();

            float step = 1 / _config.splineSegmentNumber;
            int stepsIn1Frame = 0;
            Vector3 prevPosition = Vector3.negativeInfinity;
            for (float t = 0; t <= 1; t += step)
            {
                if (stepsIn1Frame >= _config.splineSegmentNumberInOneFrame)
                {
                    await Task.Yield();
                    stepsIn1Frame = 0;
                }

                stepsIn1Frame++;

                Vector3 positionAtSpline = spline.EvaluatePosition(t);
                float dist = Vector3.Distance(positionAtSpline, prevPosition);
                
                if (dist >= _config.distanceBetweenSprites)
                {
                    SpriteRenderer renderer = spriteRendererPool.Get();
                    renderer.transform.position = positionAtSpline;
                    renderer.sortingOrder = sortOrder;
                    
                    activeSpriteRenderers.Add(renderer);
                    
                    prevPosition = positionAtSpline;
                }
            }

            InitRenderers();
            _animation.PlayAnimation(activeSpriteRenderers);
        }

        private void InitRenderers()
        {
            activeSpriteRenderers[^1].sprite = starSprite;
            for (int i = 0; i < activeSpriteRenderers.Count - 1; i++)
            {
                activeSpriteRenderers[i].sprite = circleSprite;
            }
        }

        private void ReleaseAllRenderers()
        {
            foreach (SpriteRenderer spriteRenderer in activeSpriteRenderers)
            {
                spriteRendererPool.Release(spriteRenderer);
            }
        }

        private void Awake()
        {
            spriteRendererPool = new ObjectPool<SpriteRenderer>(CreateSpriteRenderer, SpriteRendererGet, SpriteRendererRelease);
        }

        private void SpriteRendererRelease(SpriteRenderer renderer)
        {
            renderer.gameObject.SetActive(false);
        }

        private void SpriteRendererGet(SpriteRenderer renderer)
        {
            renderer.gameObject.SetActive(true);
            renderer.color = new Color(255, 255, 255, 0);
        }

        private SpriteRenderer CreateSpriteRenderer()
        {
            SpriteRenderer renderer = _assetProviderService.Instantiate<SpriteRenderer>(Constants.PathSpriteRender);
            return renderer;
        }
    }
}