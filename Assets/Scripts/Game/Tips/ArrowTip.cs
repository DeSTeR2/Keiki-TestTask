using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tips
{
    public class ArrowTip : MonoBehaviour
    {
        [SerializeField] private float minSpeed = 3f;
        [SerializeField] private float maxSpeed = 8f;

        private const float DistanceThreshold = 0.1f;
        
        private float speed;
        private int currentPositionIndex = 0;
        private List<Vector3> _poinst;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _poinst[currentPositionIndex], speed);

            if (Vector3.Distance(transform.position, _poinst[currentPositionIndex]) < DistanceThreshold)
            {
                currentPositionIndex = (currentPositionIndex + 1);
                if (currentPositionIndex >= _poinst.Count)
                {
                    ResetTip();
                }
            }
        }

        public void Move(List<Vector3> poinst)
        {
            _poinst = poinst;
            gameObject.SetActive(true);
            
            speed = Random.Range(minSpeed, maxSpeed);
            ResetTip();
        }

        public void Stop()
        {
            gameObject.SetActive(false);
        }

        private void ResetTip()
        {
            currentPositionIndex = 0;
            transform.position = _poinst[currentPositionIndex];
        }
    }
}