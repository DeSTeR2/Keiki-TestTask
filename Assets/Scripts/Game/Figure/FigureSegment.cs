using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Figure;
using Infrastructure.Events;
using Systems;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using Zenject;
using EventType = Infrastructure.Events.EventType;

public class FigureSegment : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;

    public Action OnSegmentCleared;
    public int SegmentNumber { get; private set; }

    private CameraRayCastSystem _rayCastSystem;
    private Color _objectColor;
    private GameConfig _config;
    private SegmentPath _segmentPath;
    private float clearedSegmentsNumber = -1;
    private EventHolder _dragEnded;
    private List<Vector3> _points;
    private PositionEvaluator _positionEvaluator;

    [Inject]
    public void Construct(CameraRayCastSystem RayCastSystem, GameConfig config,
        SegmentPath segmentPath, AllEvents allEvents)
    {
        _dragEnded = allEvents[EventType.DragEnded];
        _dragEnded.Event += DragEnded;

        _segmentPath = segmentPath;
        _config = config;
        _rayCastSystem = RayCastSystem;
    }

    public void SetColor(Color objectColor)
        => _objectColor = objectColor;

    public void SetNumber(int number)
    {
        SegmentNumber = number;
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            _segmentPath.CreatePath(_splineContainer, SegmentNumber - 1);
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
        for (float t = 0f; t <= 1f; t += _config.evaluateStep)
        {
            Vector3 point = _splineContainer.EvaluatePosition(t);
            _points.Add(point);
        }

        _positionEvaluator = new PositionEvaluator(_points);
    }

    private void CheckClickCollision(Ray ray)
    {
        Tuple<int, float> collisionTuple = _positionEvaluator.FindClickedIndex(ray);
        if (collisionTuple.Item2 >= _config.MinDistToSegmentPoint)
        {
            _dragEnded?.Invoke();
            return;
        }
        
        Debug.Log($"Clicked segment #{collisionTuple.Item1} at dist {collisionTuple.Item2}");
        clearedSegmentsNumber = Mathf.Max(clearedSegmentsNumber, collisionTuple.Item1);
        CheckForSegmentClear();
    }

    private void CheckForSegmentClear()
    {
        if (clearedSegmentsNumber >= _points.Count)
        {
            OnSegmentCleared?.Invoke();
        }
    }

    private void DragEnded()
    {
        clearedSegmentsNumber = -1;
    }

    private void OnDestroy()
    {
        _dragEnded.Event -= DragEnded;
    }

    void OnDrawGizmos()
    {
        if (_splineContainer == null)
            return;

        Gizmos.color = _config.toleranceRadiusColor;

        for (float t = 0; t < 1f; t += 0.01f)
        {
            Vector3 point = _splineContainer.EvaluatePosition(t);
            Gizmos.DrawSphere(point, _config.clickTolerance);
        }
    }
}