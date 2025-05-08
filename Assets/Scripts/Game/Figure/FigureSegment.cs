using System;
using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using Zenject;

public class FigureSegment : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] float clickTolerance = 0.2f;
    [SerializeField] private float evaluateStep = 0.1f;

    [Header("Gizmo")] [SerializeField] Color toleranceRadiusColor = new Color(1f, 0f, 0f, 0.3f);

    
    private List<Vector3> _points;
    private float _sqTolerance;
    private CameraRayCastSystem _rayCastSystem;
    private Color _objectColor;

    [Inject]
    public void Construct(CameraRayCastSystem RayCastSystem)
    {
        _rayCastSystem = RayCastSystem;
    }

    public void SetColor(Color objectColor) 
        => _objectColor = objectColor;

    public void SetActive(bool active)
    {
        if (active)
        {
            _rayCastSystem.OnDragging += CheckClickCollision;
        }
        else
        {
            _rayCastSystem.OnDragging -= CheckClickCollision;
        }
    }

    private void Awake()
    {
        _points = new List<Vector3>();
        _sqTolerance = clickTolerance * clickTolerance;
        for (float t = 0f; t <= 1f; t += evaluateStep)
        {
            Vector3 point = _splineContainer.EvaluatePosition(t);
            _points.Add(point);
        }
    }

    private void CheckClickCollision(Ray ray)
    {
        Vector3 rayOrigin = ray.origin;
        Vector3 rayDirection = ray.direction;

        for (int i = 0; i < _points.Count; i++)
        {
            Vector3 point = _points[i];
            Vector3 toPoint = point - rayOrigin;

            float sqDistance = Vector3.Cross(rayDirection, toPoint).sqrMagnitude / rayDirection.sqrMagnitude;

            if (sqDistance < _sqTolerance)
            {
                Debug.Log($"Object name {name}\nSpline clicked at segment #{i}");
                return;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (_splineContainer == null)
            return;

        Gizmos.color = toleranceRadiusColor;

        for (float t = 0; t < 1f; t += 0.01f)
        {
            Vector3 point = _splineContainer.EvaluatePosition(t);
            Gizmos.DrawSphere(point, clickTolerance);
        }
    }
}