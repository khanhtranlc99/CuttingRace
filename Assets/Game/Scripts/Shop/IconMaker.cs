using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class IconMaker : MonoBehaviour
{
    // Start is called before the first frame update
    public string path;
    public GameObject[] Target;
    public Vector3 angleOfPhoto;
    void Start()
    {
        //RuntimePreviewGenerator.PreviewRenderCamera = Camera
        RuntimePreviewGenerator.PreviewDirection = angleOfPhoto;
        RuntimePreviewGenerator.Padding = 0;
        RuntimePreviewGenerator.BackgroundColor = new Color(0, 0, 0, 0);
        RuntimePreviewGenerator.OrthographicMode = true;
        RuntimePreviewGenerator.MarkTextureNonReadable = false;
        path = Application.dataPath + "/Texture2D/Character/";
        Texture2D bTexture = null;

        for (int i = 0; i < Target.Length; i++)
        {
            while (bTexture == null)
            {
                bTexture = RuntimePreviewGenerator.GenerateModelPreview(Target[i].transform, 256, 256, false);
                Thread.Sleep(80);
            }

            if (bTexture != null)
            {
                bTexture.mipMapBias = -1.5f;
                bTexture.Apply();
                byte[] data = bTexture.EncodeToPNG();
                string pathAndName = path + "Image"+i.ToString() + ".png";
                File.WriteAllBytes(pathAndName, data);
            }
            bTexture = null;
        }
    }
}
