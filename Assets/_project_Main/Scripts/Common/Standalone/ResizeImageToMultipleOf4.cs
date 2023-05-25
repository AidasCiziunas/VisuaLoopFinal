using System;
using System.IO;
using UnityEngine;

public class ResizeImageToMultipleOf4 : MonoBehaviour
{
    [SerializeField] string rootDirectoryPath;

    public void OnClickResizeImage()
    {
        rootDirectoryPath = rootDirectoryPath.Replace("\\", "/");
        Debug.Log(rootDirectoryPath);

        string[] files = Directory.GetFiles(rootDirectoryPath, "*.png");

        foreach (var entry in files)
        {
            Debug.Log(entry);
            byte[] bytes = File.ReadAllBytes(Path.Combine(rootDirectoryPath, entry));
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);

            if (texture.width % 4 != 0 || texture.height % 4 != 0)
            {
                int newWidth = GetMultipleOf4Num(texture.width);
                int newHeight = GetMultipleOf4Num(texture.height);

                Texture2D newTex = ScaleTexture(texture, newWidth, newHeight);
                byte[] bytes1 = newTex.EncodeToPNG();
                File.WriteAllBytes(entry, bytes1);
            }
            else
            {
                byte[] bytes1 = texture.EncodeToPNG();
                File.WriteAllBytes(entry, bytes1);
            }
        }
    }

    private int GetMultipleOf4Num(int num)
    {
        int value = num;
        int factor = 4;
        int nearestMultiple =
                (int)Math.Round(
                     (value / (double)factor),
                     MidpointRounding.AwayFromZero
                 ) * factor;

        return nearestMultiple;
    }

    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
        float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);
        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth),
                              incY * ((float)Mathf.Floor(px / targetWidth)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        return result;
    }
}
