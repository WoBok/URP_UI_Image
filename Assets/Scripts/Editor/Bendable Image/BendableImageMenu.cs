using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BendableImageMenu : MonoBehaviour
{
    [MenuItem("GameObject/UI/Bendable Image")]
    static void CreateImage()
    {
        var activeTransform = Selection.activeTransform;
        GameObject canvasGo = null;
        bool hasCanvas = false;
        if (activeTransform != null)
        {
            Canvas canvas = activeTransform.GetComponent<Canvas>();
            if (canvas != null)
                hasCanvas = true;
            else
            {
                var parent = activeTransform.parent;
                while (parent != null)
                {
                    var currentCanvas = parent.GetComponent<Canvas>();
                    if (currentCanvas != null)
                    {
                        hasCanvas = true;
                        break;
                    }
                    parent = parent.parent;
                }
            }
        }

        if (hasCanvas)
            canvasGo = activeTransform.gameObject;

        if (!hasCanvas)
        {
            canvasGo = new GameObject("Canvas");
            canvasGo.AddComponent<Canvas>();
            canvasGo.AddComponent<CanvasScaler>();
            canvasGo.AddComponent<GraphicRaycaster>();
            canvasGo.transform.SetParent(activeTransform);
        }

        var bendableImageGo = new GameObject("Bendable Image");
        bendableImageGo.transform.SetParent(canvasGo.transform, false);
        bendableImageGo.AddComponent<BendableImage>();

        Selection.activeTransform = bendableImageGo.transform;

        Undo.RegisterCreatedObjectUndo(canvasGo, "Create " + bendableImageGo.name);
    }
}