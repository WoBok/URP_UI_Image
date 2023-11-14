using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(PentagonImage))]
    [CanEditMultipleObjects]
    public class PentagonImageEditor : GraphicEditor
    {
        SerializedProperty m_Sprite;
        SerializedProperty m_SplitDelta;
        GUIContent m_SpriteContent;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_SpriteContent = EditorGUIUtility.TrTextContent("Source Image");

            m_Sprite = serializedObject.FindProperty("m_Sprite");

            m_SplitDelta = serializedObject.FindProperty("m_SplitDelta");

            SetShowNativeSize(true);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SpriteGUI();
            AppearanceControlsGUI();
            RaycastControlsGUI();
            MaskableControlsGUI();

            SetShowNativeSize(false);
            NativeSizeButtonGUI();

            EditorGUILayout.Slider(m_SplitDelta, -0, 1, new GUIContent("SplitDelta"));

            serializedObject.ApplyModifiedProperties();
        }

        void SetShowNativeSize(bool instant)
        {
            SetShowNativeSize(true, instant);
        }

        protected void SpriteGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_Sprite, m_SpriteContent);
            if (EditorGUI.EndChangeCheck())
            {
                (serializedObject.targetObject as PentagonImage).DisableSpriteOptimizations();
            }
        }
    }
}
