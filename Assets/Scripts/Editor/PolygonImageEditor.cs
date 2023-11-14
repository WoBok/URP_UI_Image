using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(PolygonImage))]
    [CanEditMultipleObjects]
    public class PolygonImageEditor : GraphicEditor
    {
        SerializedProperty m_Sprite;
        SerializedProperty m_SidesCount;
        GUIContent m_SpriteContent;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_SpriteContent = EditorGUIUtility.TrTextContent("Source Image");

            m_Sprite = serializedObject.FindProperty("m_Sprite");

            m_SidesCount = serializedObject.FindProperty("m_SidesCount");

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

            EditorGUILayout.IntSlider(m_SidesCount, 3, 100, new GUIContent("Sides Count"));

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
                (serializedObject.targetObject as PolygonImage).DisableSpriteOptimizations();
            }
        }
    }
}