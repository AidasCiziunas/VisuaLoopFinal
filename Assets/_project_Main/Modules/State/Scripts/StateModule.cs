using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.StateModule
{
    public class StateModule : MonoBehaviour
    {
        private void OnEnable()
        {
            StateModuleSignals.Get<StateSignal.Setup>().AddListener(OnSetup);
        }

        private void OnDisable()
        {
            StateModuleSignals.Get<StateSignal.Setup>().RemoveListener(OnSetup);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            Task<(Exception, bool)> gameStateSetup = StateModuleSignals.Get<GameStateSignal.Setup>().Dispatch();

            if (gameStateSetup != null)
                await gameStateSetup;

            Debug.Log("StateModule Initialization");

            return (null, true);
        }
    }
}
