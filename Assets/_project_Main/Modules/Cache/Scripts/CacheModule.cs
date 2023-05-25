using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.CacheModule
{
    public class CacheModule : MonoBehaviour
    {
        private void OnEnable()
        {
            CacheModuleSignals.Get<CacheSignal.Setup>().AddListener(OnSetup);
        }

        private void OnDisable()
        {
            CacheModuleSignals.Get<CacheSignal.Setup>().RemoveListener(OnSetup);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            await CacheModuleSignals.Get<Tex2dCacheSignal.Setup>().Dispatch();
            await CacheModuleSignals.Get<RenderTexCacheSignal.Setup>().Dispatch();
            return (null, true);
        }
    }
}
