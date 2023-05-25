using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.SmallModule
{
    internal class TextureHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            SmallModuleSignals.Get<TextureSignal.Setup>().AddListener(OnSetup);
            SmallModuleSignals.Get<TextureSignal.ConvertTextureToTexture2D>().AddListener(OnConvertTextureToTexture2D);
        }

        private void OnDisable()
        {
            SmallModuleSignals.Get<TextureSignal.Setup>().RemoveListener(OnSetup);
            SmallModuleSignals.Get<TextureSignal.ConvertTextureToTexture2D>().RemoveListener(OnConvertTextureToTexture2D);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            return (null, true);
        }

        private Texture2D OnConvertTextureToTexture2D(Texture texture)
        {
            Texture mainTexture = texture;
            Texture2D texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);

            RenderTexture currentRT = RenderTexture.active;

            RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
            Graphics.Blit(mainTexture, renderTexture);

            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();

            Color[] pixels = texture2D.GetPixels();

            RenderTexture.active = currentRT;
            renderTexture.Release();

            return texture2D;
        }
    }
}
