namespace UnityEngine.UI
{
    public class PentagonImage : Image
    {
        [SerializeField]
        float m_SplitDelta=0.45f;
        public float splitDelta
        {
            get => m_SplitDelta;
            set
            {
                if (m_SplitDelta != value)
                {
                    m_SplitDelta = value;
                    SetVerticesDirty();
                }
            }
        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();

            var rectWidth = rectTransform.rect.width;
            var rectHeight = rectTransform.rect.height;
            var xPoint = rectWidth / 2;
            var yPoint = rectHeight / 2;

            var point1 = new Vector2(-xPoint, yPoint);
            var point2 = new Vector2(xPoint, yPoint);
            var point3 = new Vector2(-xPoint, yPoint - splitDelta * rectHeight);
            var point4 = new Vector2(xPoint, yPoint - splitDelta * rectHeight);
            var point5 = new Vector2(0, -yPoint);

            toFill.AddVert(point1, color, new Vector2(0, 1));
            toFill.AddVert(point2, color, new Vector2(1, 1));
            toFill.AddVert(point3, color, new Vector2(0, 1 - splitDelta));
            toFill.AddVert(point4, color, new Vector2(1, 1 - splitDelta));
            toFill.AddVert(point5, color, new Vector2(0.5f, 0));

            toFill.AddTriangle(0, 1, 2);
            toFill.AddTriangle(3, 2, 1);
            toFill.AddTriangle(4, 2, 3);
        }
    }
}