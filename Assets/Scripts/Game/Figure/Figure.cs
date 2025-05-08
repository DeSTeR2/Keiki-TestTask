using System;
using System.Collections.Generic;
using Infrastructure.Events;
using UnityEngine;
using Zenject;
using EventType = Infrastructure.Events.EventType;

namespace Game.Figure
{
    public class Figure : MonoBehaviour
    {
        [SerializeField] private Transform segmentParent;

        private int activeSegment = 0;
        private List<FigureSegment> _segments;
        private Color _objectColor;
        private EventHolder _levelCleared;

        [Inject]
        public void Construct(AllEvents allEvents)
        {
            _levelCleared = allEvents[EventType.LevelCleared];
        }

        public void InitFigure(Color objectColor)
        {
            _objectColor = objectColor;
            _segments = new();
            for (int i = 0; i < segmentParent.childCount; i++)
            {
                FigureSegment segment = segmentParent.GetChild(i).gameObject.GetComponent<FigureSegment>();
                segment.SetColor(_objectColor);
                segment.SetNumber(i + 1);
                segment.OnSegmentCleared += SegmentCleared;

                _segments.Add(segment);
            }

            ChangeSegmentActive(activeSegment);
        }

        private void SegmentCleared()
        {
            activeSegment++;
            ChangeSegmentActive(activeSegment);
        }

        private void ChangeSegmentActive(int activeSegment)
        {
            if (activeSegment >= _segments.Count)
            {
                _levelCleared?.Invoke();
                Debug.Log("WIN!");
                return;
            }

            Debug.Log($"New segment {activeSegment}");
            for (int i = 0; i < _segments.Count; i++)
            {
                bool active = i == activeSegment;
                _segments[i].SetActive(active);
            }
        }
    }
}