using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Polygon))]
public class PolygonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Preview"))
        {
            var polygon = Selection.activeGameObject.GetComponent<Polygon>();
            polygon.Render();
        }
    }
}