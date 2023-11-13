using System.Collections.Generic;

namespace UnityEngine.UI
{
    public class PolygonImage : Image
    {
        [SerializeField] int m_SidesCount = 6;
        public int sidesCount
        {
            get { return m_SidesCount; }
            set
            {
                if (value >= 3)
                {
                    if (m_SidesCount != value)
                    {
                        m_SidesCount = value;
                        SetVerticesDirty();
                    }
                }
                else
                    Debug.Log("边数必须大于等于3！");
            }
        }
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
            var angle = 360f / m_SidesCount * Mathf.Deg2Rad;
            var currentAngle = 90f * Mathf.Deg2Rad;
            float x;
            float y;
            for (int i = 0; i < m_SidesCount; i++)
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

            if (m_SidesCount > 3)
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
            if (m_SidesCount > 3)
            {
                for (int i = 0; i < m_SidesCount; i++)
                {
                    indices.Add(i);
                    indices.Add(m_SidesCount);
                    indices.Add((i + 1) % m_SidesCount);
                }
            }
            else
                indices.AddRange(new int[] { 0, 1, 2 });
            return indices;
        }
    }
}
