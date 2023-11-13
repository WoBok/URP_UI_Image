using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Bendable Image")]
public class BendableImage : Image
{
    [SerializeField] float m_Curvature = 0.2f;
    public float curvature
    {
        get => m_Curvature;
        set
        {
            if (m_Curvature != value)
            {
                m_Curvature = value;
                SetVerticesDirty();
            }
        }
    }
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        GenerateCurvedSprite(toFill);
    }
    void GenerateCurvedSprite(VertexHelper toFill)
    {
        toFill.Clear();

        var rectWidth = rectTransform.rect.width;
        var rectHeight = rectTransform.rect.height;

        var xPoint = rectWidth / 2;
        var yPoint = rectHeight / 2;

        var leftStartPos = new Vector3(-xPoint, -yPoint);
        var leftMiddlePos = new Vector3(-xPoint, 0, curvature * -rectHeight);
        var leftEndPos = new Vector3(-xPoint, yPoint);
        List<Vector3> leftLinePoints = BezierCurve.GetCurvePoints(new Vector3[] { leftStartPos, leftMiddlePos, leftEndPos }, 30);
        var rightStartPos = new Vector3(xPoint, -yPoint);
        var rightMiddlePos = new Vector3(xPoint, 0, curvature * -rectHeight);
        var rightEndPos = new Vector3(xPoint, yPoint);
        List<Vector3> rightLinePoints = BezierCurve.GetCurvePoints(new Vector3[] { rightStartPos, rightMiddlePos, rightEndPos }, 30);

        List<UIVertex> vertices = new List<UIVertex>();
        vertices.AddRange(GetUIVertex(leftLinePoints, color, rectWidth, rectHeight));
        vertices.AddRange(GetUIVertex(rightLinePoints, color, rectWidth, rectHeight));

        List<int> indices = new List<int>();
        for (int i = 0; i < leftLinePoints.Count - 1; i++)
        {
            indices.Add(i + leftLinePoints.Count);
            indices.Add(i + 1);
            indices.Add(i);
        }
        for (int i = rightLinePoints.Count; i < rightLinePoints.Count * 2 - 1; i++)
        {
            indices.Add(i);
            indices.Add(i + 1);
            indices.Add(i - leftLinePoints.Count + 1);
        }

        toFill.AddUIVertexStream(vertices, indices);
    }
    List<UIVertex> GetUIVertex(List<Vector3> points, Color color, float width, float hight)
    {
        List<UIVertex> uiVertices = new List<UIVertex>();
        for (int i = 0; i < points.Count; i++)
        {
            UIVertex v = new UIVertex();
            v.color = color;
            v.position = points[i];
            v.uv0 = new Vector2(points[i].x / width + 0.5f, points[i].y / hight + 0.5f);
            uiVertices.Add(v);
        }
        return uiVertices;
    }
}