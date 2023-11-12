using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolygonImage : Image
{
    public int sidesCount = 6;
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        toFill.Clear();
        toFill.AddUIVertexStream(GetUIVertex(), GetIndices());
    }
    List<UIVertex> GetUIVertex()
    {
        var rectWidth = rectTransform.rect.width;
        var rectHeight = rectTransform.rect.height;

        var vertices = new List<UIVertex>();
        var angle = 360f / sidesCount * Mathf.Deg2Rad;
        var currentAngle = -90f * Mathf.Deg2Rad;
        float x;
        float y;
        for (int i = 0; i < sidesCount; i++)
        {
            var vertex = new UIVertex();

            var cosValue = Mathf.Cos(currentAngle);
            var sinValue = Mathf.Sin(currentAngle);

            currentAngle += angle;

            x = Mathf.Sqrt(2) * cosValue;
            y = Mathf.Sqrt(2) * sinValue;
            x -= x / 2 * rectWidth;
            y -= y / 2 * rectHeight;

            vertex.color = color;
            vertex.position = new Vector3(x, y, 0);
            vertex.uv0 = new Vector2(cosValue, sinValue) / 2 + Vector2.one * 0.5f;
            vertex.normal = Vector3.back;

            vertices.Add(vertex);
        }

        if (sidesCount > 3)
        {
            var centerVertex = new UIVertex();
            centerVertex.color = color;
            centerVertex.position = Vector3.zero;
            centerVertex.uv0 = Vector2.one * 0.5f;
            centerVertex.normal = Vector3.back;
            vertices.Add(centerVertex);
        }

        return vertices;
    }
    List<int> GetIndices()
    {
        var indices = new List<int>();
        if (sidesCount > 3)
        {
            for (int i = 0; i < sidesCount; i++)
            {
                indices.Add(i);
                indices.Add(sidesCount);
                indices.Add((i + 1) % sidesCount);
            }
        }
        else
            indices.AddRange(new int[] { 0, 1, 2 });
        return indices;
    }
}