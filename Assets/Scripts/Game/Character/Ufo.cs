using System;
using UnityEngine;

namespace Game.Character
{
    public class Ufo : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private Vector3 targetPosition;

        public void NewPosition(Vector3 position)
        {
            targetPosition = position;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }

    }
}