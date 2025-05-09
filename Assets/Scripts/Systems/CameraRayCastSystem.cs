using System;
using Infrastructure.Events;
using UnityEngine;
using Zenject;
using EventType = Infrastructure.Events.EventType;

namespace Systems
{
    public class CameraRayCastSystem : MonoBehaviour
    {
        public Action<Ray> OnDragging;

        private bool isButtonPressed;
        private Vector3 prevMousePosition = Vector3.negativeInfinity;
        private EventHolder _dragEndedEvent;

        [Inject]
        public void Construct(AllEvents allEvents)
        {
            _dragEndedEvent = allEvents[EventType.DragEnded];
        }
        
        private void Update()
        {
            HandleClickLogic();
            FireRayIfNeeded();
        }

        private void FireRayIfNeeded()
        {
            Vector3 mousePosition = Input.mousePosition;
            if (!isButtonPressed || mousePosition == prevMousePosition) return;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            OnDragging?.Invoke(ray);

            prevMousePosition = mousePosition;
        }

        private void HandleClickLogic()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isButtonPressed = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isButtonPressed = false;
                _dragEndedEvent?.Invoke();
            }
        }
    }
}