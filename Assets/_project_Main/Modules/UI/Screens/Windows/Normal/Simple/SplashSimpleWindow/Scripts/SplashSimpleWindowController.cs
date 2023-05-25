using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.SplashSimpleWindow
{
    public class SplashSimpleWindowController : AWindowController<SplashSimpleWindowProperties>
    {
        [SerializeField] LoadingVisualWidgetController _loadingVisWidCtrl;

        private float _loadingTime = 2.0f;
        private IEnumerator _timerEnmr;
        private Coroutine _timerCo;

        private void OnEnable()
        {
            if (_timerEnmr != null)
                _timerCo = StartCoroutine(_timerEnmr);
        }

        private void OnDisable()
        {
            if (_timerCo != null)
                StopCoroutine(_timerCo);
        }

        protected override void OnPropertiesSet()
        {
            base.OnPropertiesSet();

            if (gameObject.activeInHierarchy == false)
                _timerEnmr = TimerCoroutine();
            else
                _timerCo = StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(_loadingTime);

            _timerCo = null;
            _timerEnmr = null;

            TargetResolution tarRes = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();

            if (tarRes == TargetResolution.Tablet)
                UIModuleSignals.Get<SplashSimpleWindowSignal.ChangeToNewScreen>().Dispatch(ScreenIds.MainMenuSimpleWindow, null, ScreenIds.InteractionToolsPanel, null, ScreenIds.None);
            else if (tarRes == TargetResolution.Phone)
                UIModuleSignals.Get<SplashSimpleWindowSignal.ChangeToNewScreen>().Dispatch(ScreenIds.MainMenuSimpleWindow_Mob, null, ScreenIds.InteractionToolsPanel_Mob, null, ScreenIds.None);
        }
    }
}
