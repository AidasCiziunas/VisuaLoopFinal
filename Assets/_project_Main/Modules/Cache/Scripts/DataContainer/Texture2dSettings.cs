using UnityEngine;

namespace SyedAli.Main.CacheModule
{
    public class Tex2dData
    {
        public readonly int Width;
        public readonly int Height;
        public readonly TextureFormat TexFormat;
        public readonly Texture2D Tex2d;

        public Tex2dData(int width, int height, TextureFormat textureFormat, Texture2D tex2d)
        {
            Width = width;
            Height = height;
            TexFormat = textureFormat;
            Tex2d = tex2d;
        }
    }
}
