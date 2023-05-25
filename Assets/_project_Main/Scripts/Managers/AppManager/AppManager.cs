using System;
using System.Threading.Tasks;
using UnityEngine;
using static SyedAli.Main.AppManagerSignal;

namespace SyedAli.Main
{
    public class AppManager : PersistentSingleton<AppManager>
    {
        [SerializeField] private BuildType _buildType;

        [Header("Testing")]
        [SerializeField] private TargetResolution _targetResolution;
        [Space(10)]

        private Task<(Exception, bool)> _stateModuleSetupTask;
        private Task<(Exception, bool)> _langaugeModuleSetupTask;
        private Task<(Exception, bool)> _smallModuleSetupTask;
        private Task<(Exception, bool)> _uiManagerSetupTask;

        protected override void Awake()
        {
            base.Awake();

#if !UNITY_EDITOR
            _targetResolution = CheckIphoneOrIpad();
#endif
        }

        private void OnEnable()
        {
            ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().AddListener(OnGetTargetDevice);
        }

        private void OnDisable()
        {
            ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().RemoveListener(OnGetTargetDevice);
        }

        private async void Start()
        {
            Application.targetFrameRate = 60;

            _langaugeModuleSetupTask = LanguageModuleSignals.Get<LanguageSignal.Setup>().Dispatch();
            _smallModuleSetupTask = SmallModuleSignals.Get<SmallSignal.Setup>().Dispatch();

            if (_langaugeModuleSetupTask != null) await _langaugeModuleSetupTask;
            if (_smallModuleSetupTask != null) await _smallModuleSetupTask;

            _stateModuleSetupTask = StateModuleSignals.Get<StateSignal.Setup>().Dispatch();
            if (_stateModuleSetupTask != null) await _stateModuleSetupTask;

            _uiManagerSetupTask = ManagersSignals.Get<UIManagerSignal.Setup>().Dispatch(_buildType, _targetResolution, true);
            if (_uiManagerSetupTask != null)
                await _uiManagerSetupTask;

            SmallModuleSignals.Get<DebuggingSignal.ColorLog>().Dispatch("All Modules Initialized", DebugColor.green);
            ManagersSignals.Get<AppManagerSignal.AllModulesInitialized>().Dispatch();

            Screen.orientation = ScreenOrientation.Portrait;
        }

        private TargetResolution OnGetTargetDevice()
        {
            return _targetResolution;
        }

        private void OnDestroy()
        {
            _stateModuleSetupTask?.Dispose();
            _langaugeModuleSetupTask?.Dispose();
            _smallModuleSetupTask?.Dispose();
            _uiManagerSetupTask?.Dispose();
        }

        private TargetResolution CheckIphoneOrIpad()
        {
            var identifier = SystemInfo.deviceModel;
            if (identifier.StartsWith("iPhone"))
            {
                Debug.Log("Iphone Target Found");
                return TargetResolution.Phone;
            }
            else if (identifier.StartsWith("iPad"))
            {
                Debug.Log("Ipad Target Found");
                return TargetResolution.Tablet;
            }
            else
            {
                return TargetResolution.Tablet;
            }
        }
    }
}

