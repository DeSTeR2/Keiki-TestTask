using System.Collections.Generic;
using Game.Character;
using Infrastructure.Events;
using UnityEngine;
using EventType = Infrastructure.Events.EventType;

namespace Game.Figure.Segment
{
    public class SegmentFiller : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        private SegmentEvaluator _evaluator;
        private Ufo _character;
        private AllEvents _allEvents;

        public void Init(Color objectColor, int sortOrder, SegmentEvaluator evaluator,
            Ufo character, AllEvents allEvents)
        {
            _allEvents = allEvents;
            _character = character;
            _evaluator = evaluator;
            ConfigureLineRenderer(objectColor, sortOrder);
            
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _character.NewPosition(_evaluator.Points[0]);
        }

        public void FillSegment(int clearedSegmentsNumber)
        {
            while (lineRenderer.positionCount < clearedSegmentsNumber)
                AddNewPosition();

            _allEvents[EventType.DraggingApproved]?.Invoke();
        }

        private void ConfigureLineRenderer(Color objectColor, int sortOrder)
        {
            Gradient gradient = new Gradient
            {
                colorKeys = new GradientColorKey[]
                {
                    new GradientColorKey(objectColor, 0),
                }
            };

            lineRenderer.sortingOrder = sortOrder;
            lineRenderer.colorGradient = gradient;
            lineRenderer.positionCount = 0;
        }

        private void AddNewPosition()
        {
            int positionIndex = lineRenderer.positionCount;
            lineRenderer.positionCount = positionIndex + 1; 
            lineRenderer.SetPosition(positionIndex, _evaluator.Points[positionIndex]);

            _character.NewPosition(_evaluator.Points[positionIndex]);
        }
    }
}