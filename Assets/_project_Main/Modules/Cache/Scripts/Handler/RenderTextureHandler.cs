using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.CacheModule
{
    internal class RenderTextureHandler : MonoBehaviour
    {
        private List<RenderTexData> _renderTexDataLs = new List<RenderTexData>();

        private void OnEnable()
        {
            CacheModuleSignals.Get<RenderTexCacheSignal.Setup>().AddListener(OnSetup);
            CacheModuleSignals.Get<RenderTexCacheSignal.GetRenderTex>().AddListener(OnGetRenderTex);
            CacheModuleSignals.Get<RenderTexCacheSignal.ReleaseAllRenderTex>().AddListener(OnReleaseAllRenderTex);
        }

        private void OnDisable()
        {
            CacheModuleSignals.Get<RenderTexCacheSignal.Setup>().RemoveListener(OnSetup);
            CacheModuleSignals.Get<RenderTexCacheSignal.GetRenderTex>().RemoveListener(OnGetRenderTex);
            CacheModuleSignals.Get<RenderTexCacheSignal.ReleaseAllRenderTex>().RemoveListener(OnReleaseAllRenderTex);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            return (null, true);
        }

        private RenderTexture OnGetRenderTex(int width, int height, int depth)
        {
            for (int i = 0; i < _renderTexDataLs.Count; i++)
            {
                if (_renderTexDataLs[i].Width == width && _renderTexDataLs[i].Height == height && _renderTexDataLs[i].Depth == depth)
                {
                    return _renderTexDataLs[i].RTex;
                }
            }

            RenderTexData rTexData = new RenderTexData(width, height, depth, new RenderTexture(width, height, depth));

            _renderTexDataLs.Add(rTexData);

            return rTexData.RTex;
        }

        private void OnReleaseAllRenderTex()
        {
            foreach (var entry in _renderTexDataLs)
            {
                Destroy(entry.RTex);
            }
            _renderTexDataLs.Clear();
        }
    }
}
