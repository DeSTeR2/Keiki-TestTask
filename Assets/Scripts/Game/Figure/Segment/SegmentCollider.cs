using System;
using System.Collections.Generic;
using Infrastructure.Events;
using UnityEngine;

namespace Game.Figure.Segment
{
    public class SegmentCollider
    {
        private FigureSegment _figureSegment;
        PositionEvaluator _positionEvaluator;

        private int _clearedSegmentsNumber = 0;
        private readonly SegmentEvaluator _segmentEvaluator;
        private SegmentFiller _segmentFiller;
        private readonly EventHolder _dragEnded;
        private readonly GameConfig _gameConfig;

        public SegmentCollider(FigureSegment figureSegment, SegmentEvaluator segmentEvaluator, 
            SegmentFiller segmentFiller, EventHolder dragEnded, GameConfig gameConfig)
        {
            _figureSegment = figureSegment;
            _segmentEvaluator = segmentEvaluator;
            _segmentFiller = segmentFiller;
            _dragEnded = dragEnded;
            _gameConfig = gameConfig;

            _positionEvaluator = new PositionEvaluator(_segmentEvaluator.Points);
        }

        public void CheckClickCollision(Ray ray)
        {
            Tuple<int, float> collisionTuple = _positionEvaluator.FindClickedIndex(ray);
            if (collisionTuple.Item2 >= _figureSegment.Config.minDistToSegmentPoint)
            {
                _dragEnded?.Invoke();
                return;
            }
        
            if (CanFillSegment(collisionTuple))
            {
                _clearedSegmentsNumber = collisionTuple.Item1;
                _segmentFiller.FillSegment(_clearedSegmentsNumber);
                CheckForSegmentClear();
            }
        }

        private bool CanFillSegment(Tuple<int, float> collisionTuple)
        {
            int sub = collisionTuple.Item1 - _clearedSegmentsNumber;
            return (sub >= 1 && sub <= _gameConfig.maxSkipSegments);
        }

        private void CheckForSegmentClear()
        {
            if (_clearedSegmentsNumber >= _segmentEvaluator.Points.Count - 1)
            {
                _figureSegment.OnSegmentCleared?.Invoke();
            }
        }
    }
}