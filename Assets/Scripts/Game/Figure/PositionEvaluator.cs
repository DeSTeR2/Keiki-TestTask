using System;
using System.Collections.Generic;
using UnityEngine;

public class PositionEvaluator
{
    private List<Vector3> _points;

    public PositionEvaluator(List<Vector3> points)
    {
        _points = points;
    }

    public Tuple<int, float> FindClickedIndex(Ray ray)
    {
        int l = 0;
        int r = _points.Count - 1;

        while (r - l > 2)
        {
            int m1 = r - (r - l) / 3;
            int m2 = l + (r - l) / 3;

            if (EvaluatePosition(m1, ray) < EvaluatePosition(m2, ray))
            {
                l = m1;
            }
            else
            {
                r = m2;
            }
        }

        float ev1 = EvaluatePosition(l, ray);
        float ev2 = EvaluatePosition(l + 1, ray);
        float ev3 = EvaluatePosition(r, ray);

        float minDist = Mathf.Min(new[] { ev1, ev2, ev3 });
        if (minDist == ev1) return new Tuple<int, float>(l + 1, minDist);
        if (minDist == ev1) return new Tuple<int, float>(l + 2, minDist);
        return new Tuple<int, float>(r + 1, minDist);
    }

    private float EvaluatePosition(int index, Ray ray)
    {
        Vector3 rayOrigin = ray.origin;
        Vector3 rayDirection = ray.direction;
        
        Vector3 point = _points[index];
        Vector3 toPoint = point - rayOrigin;

        float sqDistance = Vector3.Cross(rayDirection, toPoint).sqrMagnitude / rayDirection.sqrMagnitude;
        return sqDistance;
    }
}