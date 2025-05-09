using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Figure.Segment
{
    public class SegmentEvaluator : IDisposable
    {
        private FigureSegment _figureSegment;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public List<Vector3> Points { get; private set; }

        public SegmentEvaluator(FigureSegment figureSegment)
        {
            _figureSegment = figureSegment;
        
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        public async Task EvaluateSpline()
        {
            Points = new List<Vector3>();

            int evaluatedPositionInFrame = 0;
            int segmentsNumber = (int)(_figureSegment.Container.CalculateLength() / _figureSegment.Config.distanceBetweenSegments);
            float evaluateStep = 1f / segmentsNumber;
            for (float t = 0f; t <= 1f; t += evaluateStep)
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }

                Vector3 point = _figureSegment.Container.EvaluatePosition(t);
                Points.Add(point);

                evaluatedPositionInFrame++;

                if (evaluatedPositionInFrame >= _figureSegment.Config.splineSegmentNumberInOneFrame)
                {
                    evaluatedPositionInFrame = 0;
                    await Task.Yield();
                }
            }
        }

        public void Dispose()
        {        
            if (_cancellationToken.CanBeCanceled)
            {
                _cancellationTokenSource.Cancel();
            }

            _cancellationTokenSource.Dispose();
        }
    }
}