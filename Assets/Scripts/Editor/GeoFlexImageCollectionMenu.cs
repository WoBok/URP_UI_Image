using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GeoFlexImageCollectionMenu : MonoBehaviour
{
    [MenuItem("GameObject/UI/Blurred Image")]
    static void CreateBlurredImage()
    {
        CreateImage<BlurredImage>("Blurred Image");
    }
    [MenuItem("GameObject/UI/Bendable Image")]
    static void CreateBendableImage()
    {
        CreateImage<BendableImage>("Bendable Image");
    }
    [MenuItem("GameObject/UI/Polygon Image")]
    static void CreatePolygonImage()
    {
        CreateImage<PolygonImage>("Polygon Image");
    }
    [MenuItem("GameObject/UI/Pentagon Image")]
    static void CreatePentagonImage()
    {
        CreateImage<PentagonImage>("Pentagon Image");
    }
    [MenuItem("GameObject/UI/Arched Image")]
    static void CreateArchedImage()
    {
        CreateImage<ArchedImage>("Arched Image");
    }
    static void CreateImage<T>(string name) where T : Image
    {
        bool hasCanvas = false;

        var activeTransform = Selection.activeTransform;
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

        GameObject canvasObj = null;

        if (hasCanvas)
            canvasObj = activeTransform.gameObject;

        if (!hasCanvas)
        {
            canvasObj = new GameObject("Canvas");
            canvasObj.AddComponent<Canvas>();
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvasObj.transform.SetParent(activeTransform);
        }

        var imageObj = new GameObject(name);
        imageObj.transform.SetParent(canvasObj.transform, false);
        imageObj.AddComponent<T>();

        Selection.activeTransform = imageObj.transform;

        Undo.RegisterCreatedObjectUndo(canvasObj, "Create " + name);
    }
}