using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.CacheModule
{
    internal class Texture2dHandler : MonoBehaviour
    {
        private List<Tex2dData> _tex2dLs = new List<Tex2dData>();

        private void OnEnable()
        {
            CacheModuleSignals.Get<Tex2dCacheSignal.Setup>().AddListener(OnSetup);
            CacheModuleSignals.Get<Tex2dCacheSignal.GetTex2d>().AddListener(OnGetTex2d);
            CacheModuleSignals.Get<Tex2dCacheSignal.ReleaseAllTex2d>().AddListener(OnReleaseAllTex2d);
        }

        private void OnDisable()
        {
            CacheModuleSignals.Get<Tex2dCacheSignal.Setup>().RemoveListener(OnSetup);
            CacheModuleSignals.Get<Tex2dCacheSignal.GetTex2d>().RemoveListener(OnGetTex2d);
            CacheModuleSignals.Get<Tex2dCacheSignal.ReleaseAllTex2d>().RemoveListener(OnReleaseAllTex2d);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            return (null, true);
        }

        private Texture2D OnGetTex2d(int width, int height, TextureFormat texFormat, bool mipChain)
        {
            for (int i = 0; i < _tex2dLs.Count; i++)
            {
                if (_tex2dLs[i].Width == width && _tex2dLs[i].Height == height && _tex2dLs[i].TexFormat == texFormat)
                {
                    return _tex2dLs[i].Tex2d;
                }
            }

            Tex2dData tex2dData = new Tex2dData(width, height, texFormat, new Texture2D(width, height, texFormat, false));
            _tex2dLs.Add(tex2dData);

            return tex2dData.Tex2d;
        }

        private void OnReleaseAllTex2d()
        {
            foreach (var entry in _tex2dLs)
            {
                Destroy(entry.Tex2d);
            }
            _tex2dLs.Clear();
        }
    }
}
