using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.SmallModule
{
    public class SmallModule : MonoBehaviour
    {
        private void OnEnable()
        {
            SmallModuleSignals.Get<SmallSignal.Setup>().AddListener(OnSetup);
        }

        private void OnDisable()
        {
            SmallModuleSignals.Get<SmallSignal.Setup>().RemoveListener(OnSetup);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            await SmallModuleSignals.Get<DebuggingSignal.Setup>().Dispatch();
            await SmallModuleSignals.Get<StringSignal.Setup>().Dispatch();
            await SmallModuleSignals.Get<RectUISignal.Setup>().Dispatch();
            await SmallModuleSignals.Get<TextureSignal.Setup>().Dispatch();

            Debug.Log("SmallModule Initialization");
            return (null, true);
        }
    }
}
