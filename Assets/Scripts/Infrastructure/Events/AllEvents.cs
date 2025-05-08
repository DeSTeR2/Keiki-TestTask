using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Infrastructure.Events
{
    [CreateAssetMenu(fileName = "All events", menuName = "Events/All events")]
    public class AllEvents : ScriptableObject
    {
        private Dictionary<EventType, EventHolder> _events;

        public EventHolder this[EventType type]
        {
            get => _events[type];
        }
        
        public void InitEvents()
        {
            _events = new();
            int enumNumber = Enum.GetValues(typeof(EventType)).Length;
            for (int i = 0; i < enumNumber; i++)
            {
                EventType eventType = (EventType)i;
                _events.Add(eventType, new EventHolder());
            }
        }
    }
}
