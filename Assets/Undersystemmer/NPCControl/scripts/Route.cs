using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    private void OnDrawGizmos()
    {
        if (points == null || points.Count < 1)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(points.First().position, points.Last().position);
        Gizmos.DrawSphere(points.Last().position, 0.3f);

        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawSphere(points[i].position, 0.3f);
            if (points[i] != null && points[i + 1] != null)
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }
    }
}
