using UnityEngine;
using System.Collections;

public class ScreenShoot : MonoBehaviour
{
    public string folder = "ScreenShootFolder";

    public static ScreenShoot instanceScreenShoot = null;

    Texture2D texture2D;

    void Awake()
    {
        if (instanceScreenShoot == null)
            instanceScreenShoot = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        System.IO.Directory.CreateDirectory(folder);
    }

    void Texture2DScreenShoot(string nameFile)
    {
        texture2D = ScreenCapture.CaptureScreenshotAsTexture();
        texture2D.name = nameFile;
    }

    Texture2D GetTexture2DScreenShoot(string nameFile)
    {
        texture2D = ScreenCapture.CaptureScreenshotAsTexture();
        texture2D.name = nameFile;
        return texture2D;
    }

    Texture2D GetTexture2D()
    {
        return texture2D;
    }
}