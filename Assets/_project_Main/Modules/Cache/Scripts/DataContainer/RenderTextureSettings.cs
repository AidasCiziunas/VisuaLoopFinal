using UnityEngine;

namespace SyedAli.Main.CacheModule
{
    public class RenderTexData
    {
        public readonly int Width;
        public readonly int Height;
        public readonly int Depth;
        public readonly RenderTexture RTex;

        public RenderTexData(int width, int height, int depth, RenderTexture renderTexture)
        {
            Width = width;
            Height = height;
            Depth = depth;
            RTex = renderTexture;
        }
    }
}
