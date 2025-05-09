using System;
using System.Collections.Generic;
using Game.Character;
using Game.Figure.Segment;
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
        private Ufo _character;
        private AllEvents _allEvents;

        [Inject]
        public void Construct(AllEvents allEvents)
        {
            _allEvents = allEvents;
            _levelCleared = _allEvents[EventType.LevelCleared];
        }

        public async void InitFigure(Color objectColor)
        {
            _objectColor = objectColor;
            _segments = new();
            for (int i = 0; i < segmentParent.childCount; i++)
            {
                int sortOrder = (i + 1) * 2;
                
                FigureSegment segment = segmentParent.GetChild(i).gameObject.GetComponent<FigureSegment>();
                await segment.Init(_objectColor, sortOrder, _character, SegmentCleared, _allEvents);
                _segments.Add(segment);
            }

            ChangeSegmentActive(activeSegment);
        }

        public void SetCharacter(Ufo character)
        {
            _character = character;
        }

        public List<Vector3> GetCurrentPositions() 
            => _segments[activeSegment]?.GetPositions();
        

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
                return;
            }

            for (int i = 0; i < _segments.Count; i++)
            {
                bool active = i == activeSegment;
                _segments[i].SegmentActivator.SetActive(active);
            }
        }
    }
}