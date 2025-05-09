using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Character;
using Infrastructure.Events;
using Infrastructure.Game;
using Infrastructure.Services.Assets;
using Systems;
using UnityEngine;
using UnityEngine.Splines;
using Zenject;
using EventType = Infrastructure.Events.EventType;

namespace Game.Figure.Segment
{
    public class FigureSegment : MonoBehaviour
    {
        public SplineContainer Container;

        public Action OnSegmentCleared;

        private CameraRayCastSystem _rayCastSystem;
        private Color _objectColor;
        public GameConfig Config { get; private set; }

        private EventHolder _dragEnded;
        private IAssetProviderService _assetProviderService;

        private SegmentPath _segmentPath;
        private SegmentFiller _segmentFiller;
        private SegmentEvaluator _segmentEvaluator;
        private SegmentCollider _segmentCollider;
        public SegmentActivator SegmentActivator { get; private set; }

        [Inject]
        public void Construct(CameraRayCastSystem RayCastSystem, GameConfig config,
            SegmentPath segmentPath, AllEvents allEvents, IAssetProviderService assetProviderService)
        {
            _assetProviderService = assetProviderService;
            _dragEnded = allEvents[EventType.DragEnded];
            _dragEnded.Event += DragEnded;

            _segmentPath = segmentPath;
            Config = config;
            _rayCastSystem = RayCastSystem;
        }

        public async Task Init(Color objectColor, int sortOrder, Ufo character, Action segmentCleared, AllEvents allEvents)
        {
            _objectColor = objectColor;

            _segmentEvaluator = new SegmentEvaluator(this);
            await _segmentEvaluator.EvaluateSpline();

            _segmentFiller = _assetProviderService.Instantiate<SegmentFiller>(Constants.SegmnetFiller, transform);
            _segmentFiller.Init(_objectColor, sortOrder, _segmentEvaluator, character, allEvents);

            _segmentCollider = new SegmentCollider(this, _segmentEvaluator, _segmentFiller, _dragEnded, Config);
            SegmentActivator =
                new SegmentActivator(_segmentFiller, _segmentPath, _rayCastSystem, _segmentCollider,
                    _segmentEvaluator, sortOrder);

            OnSegmentCleared += segmentCleared;
        }

        public List<Vector3> GetPositions()
            => _segmentEvaluator?.Points;

        private void DragEnded()
        {
            // _clearedSegmentsNumber = -1;
        }

        private void OnDestroy()
        {
            _segmentEvaluator?.Dispose();
            _dragEnded.Event -= DragEnded;
        }
    }
}