using UnityEditor;
using UnityEditor.UI;

namespace UnityEngine.UI
{
    [CanEditMultipleObjects]
    public class GeoFlexImageCollectionEditor : GraphicEditor
    {
        SerializedProperty m_Sprite;
        GUIContent m_SpriteContent;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_SpriteContent = EditorGUIUtility.TrTextContent("Source Image");

            m_Sprite = serializedObject.FindProperty("m_Sprite");


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