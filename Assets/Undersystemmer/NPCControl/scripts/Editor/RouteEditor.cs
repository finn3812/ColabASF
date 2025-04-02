// Rigitg fin rute editor spørg ikke hvordan det virker, det gør det bare...
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Route))]
public class RouteEditor : Editor
{
    private bool editorEnabled = false;

    public override void OnInspectorGUI()
    {
        Route route = (Route)target;
        DrawDefaultInspector();

        editorEnabled = GUILayout.Toggle(editorEnabled, "Aktiver Route Editor");

        if (!editorEnabled)
        {
            EditorGUILayout.HelpBox("Route Editor er deaktiveret", MessageType.Info);
        }
        if (GUILayout.Button("Ryd rute"))
        {
            List<Transform> points = route.points;
            foreach (Transform t in points)
            {
                DestroyImmediate(t.gameObject);
            }
            route.points.Clear();
        }
    }

    private void OnSceneGUI()
    {
        if (!editorEnabled)
            return;

        Route route = (Route)target;
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0 && !currentEvent.alt)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 pointPosition = hit.point;
                pointPosition.y += 0.1f;

                GameObject newPoint = new GameObject("Route Point");
                Undo.RegisterCreatedObjectUndo(newPoint, "Create Route Point");
                newPoint.transform.position = pointPosition;
                newPoint.transform.parent = route.transform;

                route.points.Add(newPoint.transform);
                EditorUtility.SetDirty(route);

                currentEvent.Use();
            }
        }
    }
}
