using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Figure
{
    public class Figure : MonoBehaviour
    {
        [SerializeField] private Transform segmentParent;

        private int activeSegment = 0;
        private List<FigureSegment> _segments;
        private Color _objectColor;

        public void InitFigure(Color objectColor)
        {
            _objectColor = objectColor;
            _segments = new();
            for (int i = 0; i < segmentParent.childCount; i++)
            {
                FigureSegment segment = segmentParent.GetChild(i).gameObject.GetComponent<FigureSegment>();
                segment.SetColor(_objectColor);
                
                _segments.Add(segment);
            }
        }

        private void Start()
        {
            ChangeSegmentActive(0);
        }

        private void ChangeSegmentActive(int activeSegment)
        {
            for (int i = 0; i < _segments.Count; i++)
            {
                bool active = i == activeSegment;
                _segments[i].SetActive(active);
            }
        }
    }
}