using System.Collections.Generic;
using Systems;
using UnityEngine;

namespace Game.Figure.Segment
{
    public class SegmentActivator
    {
        private readonly SegmentFiller _segmentFiller;
        private readonly SegmentPath _segmentPath;
        private readonly CameraRayCastSystem _rayCastSystem;
        private readonly SegmentCollider _segmentCollider;
        private readonly SegmentEvaluator _evaluator;
        private readonly int _sortOrder;

        public SegmentActivator(SegmentFiller segmentFiller, SegmentPath segmentPath, 
            CameraRayCastSystem rayCastSystem, SegmentCollider segmentCollider, SegmentEvaluator evaluator, int sortOrder)
        {
            _segmentFiller = segmentFiller;
            _segmentPath = segmentPath;
            _rayCastSystem = rayCastSystem;
            _segmentCollider = segmentCollider;
            _evaluator = evaluator;
            _sortOrder = sortOrder;
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                ActivateSegment();
            }
            else
            {
                DeactivateSegment();
            }
        }

        private void ActivateSegment()
        {
            _segmentFiller.Activate();
            _segmentPath.CreatePath(_evaluator.Points, _sortOrder - 1, PathCreated);
        }
    
        private void DeactivateSegment()
        {
            _rayCastSystem.OnDragging -= _segmentCollider.CheckClickCollision;
        }
    
        private void PathCreated()
        {
            _rayCastSystem.OnDragging += _segmentCollider.CheckClickCollision;
        }
    }
}