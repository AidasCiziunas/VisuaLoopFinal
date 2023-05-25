using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.SmallModule
{
    // Green -> Module Initialize
    // Cyan -> Http Request, Yellow -> Http Response, Red -> Request Error, Orange -> Http Server Push
    // White -> Mqtt Publish Msg, Magenta -> Mqtt Server Push
    internal class DebuggingHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            SmallModuleSignals.Get<DebuggingSignal.Setup>().AddListener(OnSetup);
            SmallModuleSignals.Get<DebuggingSignal.Log>().AddListener(OnLog);
            SmallModuleSignals.Get<DebuggingSignal.ColorLog>().AddListener(OnColorLog);
            SmallModuleSignals.Get<DebuggingSignal.ColorLogObj>().AddListener(OnColorLogObj);
            SmallModuleSignals.Get<DebuggingSignal.LogStack>().AddListener(OnLogStack);
            SmallModuleSignals.Get<DebuggingSignal.LogError>().AddListener(OnLogError);
            SmallModuleSignals.Get<DebuggingSignal.LogErrorStack>().AddListener(OnLogErrorStack);
        }

        private void OnDisable()
        {
            SmallModuleSignals.Get<DebuggingSignal.Setup>().RemoveListener(OnSetup);
            SmallModuleSignals.Get<DebuggingSignal.Log>().RemoveListener(OnLog);
            SmallModuleSignals.Get<DebuggingSignal.ColorLog>().RemoveListener(OnColorLog);
            SmallModuleSignals.Get<DebuggingSignal.ColorLogObj>().RemoveListener(OnColorLogObj);
            SmallModuleSignals.Get<DebuggingSignal.LogStack>().RemoveListener(OnLogStack);
            SmallModuleSignals.Get<DebuggingSignal.LogError>().RemoveListener(OnLogError);
            SmallModuleSignals.Get<DebuggingSignal.LogErrorStack>().RemoveListener(OnLogErrorStack);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            return (null, true);
        }
        private Void OnLog(string msg)
        {
            UnityEngine.Debug.Log("Msg: " + msg);
            return null;
        }

        private Void OnColorLog(string msg, DebugColor txtColor)
        {
            if (txtColor == DebugColor.green)
                UnityEngine.Debug.Log("<color=green>" + msg + "</color>");
            else if (txtColor == DebugColor.magenta)
                UnityEngine.Debug.Log("<color=magenta>" + msg + "</color>");
            else if (txtColor == DebugColor.red)
                UnityEngine.Debug.Log("<color=red>" + msg + "</color>");
            else if (txtColor == DebugColor.cyan)
                UnityEngine.Debug.Log("<color=cyan>" + msg + "</color>");
            else if (txtColor == DebugColor.blue)
                UnityEngine.Debug.Log("<color=blue>" + msg + "</color>");
            else if (txtColor == DebugColor.yellow)
                UnityEngine.Debug.Log("<color=yellow>" + msg + "</color>");
            else if (txtColor == DebugColor.white)
                UnityEngine.Debug.Log("<color=white>" + msg + "</color>");
            else if (txtColor == DebugColor.pink)
                UnityEngine.Debug.Log("<color=#D90B91>" + msg + "</color>");
            else if (txtColor == DebugColor.orange)
                UnityEngine.Debug.Log("<color=#F4891D>" + msg + "</color>");

            return null;
        }

        private Void OnColorLogObj(object obj, DebugColor txtColor)
        {
            string msg = JsonConvert.SerializeObject(obj);

            if (txtColor == DebugColor.green)
                UnityEngine.Debug.Log("<color=green>" + msg + "</color>");
            else if (txtColor == DebugColor.magenta)
                UnityEngine.Debug.Log("<color=magenta>" + msg + "</color>");
            else if (txtColor == DebugColor.red)
                UnityEngine.Debug.Log("<color=red>" + msg + "</color>");
            else if (txtColor == DebugColor.cyan)
                UnityEngine.Debug.Log("<color=cyan>" + msg + "</color>");
            else if (txtColor == DebugColor.blue)
                UnityEngine.Debug.Log("<color=blue>" + msg + "</color>");
            else if (txtColor == DebugColor.yellow)
                UnityEngine.Debug.Log("<color=yellow>" + msg + "</color>");
            else if (txtColor == DebugColor.white)
                UnityEngine.Debug.Log("<color=white>" + msg + "</color>");
            else if (txtColor == DebugColor.pink)
                UnityEngine.Debug.Log("<color=#D90B91>" + msg + "</color>");
            else if (txtColor == DebugColor.orange)
                UnityEngine.Debug.Log("<color=#F4891D>" + msg + "</color>");

            return null;
        }

        private Void OnLogStack(string msg, object callerObj)
        {
            UnityEngine.Debug.Log("Msg: " + msg + " - Class: " + callerObj.GetType().Name + " - Function: " + GetCurrentMethod());
            return null;
        }

        private Void OnLogError(string msg)
        {
            UnityEngine.Debug.LogError("Msg: " + msg);
            return null;
        }
        private Void OnLogErrorStack(string msg, object callerObj)
        {
            UnityEngine.Debug.LogError("Msg: " + msg + " - Class: " + callerObj.GetType().Name + " - Function: " + GetCurrentMethod());
            return null;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(3);

            return sf.GetMethod().Name;
        }

    }
}
