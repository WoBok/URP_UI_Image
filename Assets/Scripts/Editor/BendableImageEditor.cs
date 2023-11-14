using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(BendableImage))]
    [CanEditMultipleObjects]
    public class BendableImageEditor : GraphicEditor
    {
        SerializedProperty m_Sprite;
        SerializedProperty m_Curvature;
        GUIContent m_SpriteContent;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_SpriteContent = EditorGUIUtility.TrTextContent("Source Image");

            m_Sprite = serializedObject.FindProperty("m_Sprite");

            m_Curvature = serializedObject.FindProperty("m_Curvature");

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

            EditorGUILayout.Slider(m_Curvature, -1, 1, new GUIContent("Curvature"));

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
                (serializedObject.targetObject as BendableImage).DisableSpriteOptimizations();
            }
        }
    }
}