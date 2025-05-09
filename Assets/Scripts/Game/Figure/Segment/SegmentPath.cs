using System;
using System.Collections.Generic;
using Infrastructure.Game;
using Infrastructure.Services.Assets;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

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
        private DiContainer _container;
        private Color _startRenderColor = new Color(255, 255, 255, 0);

        [Inject]
        public void Construct(IAssetProviderService assetProviderService, PathAnimation animation,
            GameConfig config, DiContainer container)
        {
            _container = container;
            _config = config;
            _animation = animation;
            _assetProviderService = assetProviderService;
        }

        public void CreatePath(List<Vector3> points, int sortOrder, Action onPathCreated)
        {
            ReleaseAllRenderers();
            SetRenderersPosition(points, sortOrder);
            FillRenderersWithSprite();
            
            _animation.PlayAnimation(activeSpriteRenderers, onPathCreated);
        }

        private void SetRenderersPosition(List<Vector3> points, int sortOrder)
        {
            Vector3 prevPosition = Vector3.negativeInfinity;
            for (int i = 0; i < points.Count; i ++)
            {
                Vector3 positionAtSpline = points[i];
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
        }

        private void FillRenderersWithSprite()
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
            
            activeSpriteRenderers.Clear();
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
            renderer.color = _startRenderColor;
        }

        private SpriteRenderer CreateSpriteRenderer()
        {
            SpriteRenderer renderer = _assetProviderService.Instantiate<SpriteRenderer>(Constants.PathSpriteRender, _container);
            return renderer;
        }
    }
}