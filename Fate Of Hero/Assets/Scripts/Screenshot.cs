using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    private static Screenshot instance;
    private Camera MyCamera;
    private bool takeScreenshotNextFrame;

    private void Awake()
    {
        instance = this;
        MyCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenshotNextFrame)
        {
            takeScreenshotNextFrame = false;
            RenderTexture renderTexture = MyCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            Debug.Log("Saved CameraScreenShot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            MyCamera.targetTexture = null;
        }
    }
    private void TakeScreenshot(int width, int height)
    {
        MyCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotNextFrame = true;
    }

    public static void TakeScreenshot_static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }
}
