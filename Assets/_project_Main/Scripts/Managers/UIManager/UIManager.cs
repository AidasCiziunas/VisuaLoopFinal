using Doozy.Runtime.UIManager;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SyedAli.Main
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIFlow _uIFlow;
        [SerializeField] private UISettings _defaultUISettings;
        [SerializeField] private UISettings _defaultUISettingsMob;

        [Header("Testing")]
        [SerializeField] private UISettings _testingUISettings;
        [SerializeField] private UISettings _testingUISettingsMob;
        [Space(10)]

        [SerializeField] AudioSource _bgAudSrc;
        
        private UIFrame uIFrame;
        private BuildType _buildType;
        private TargetResolution _tarRes;

        private void OnEnable()
        {
            ManagersSignals.Get<UIManagerSignal.Setup>().AddListener(OnSetup);
            ManagersSignals.Get<UIManagerSignal.ChangeToNewScreen>().AddListener(OnNavigation);
            ManagersSignals.Get<UIManagerSignal.IsWindowOnTopOfAllWindows>().AddListener(OnIsWindowOnTopOfAllWindows);
            ManagersSignals.Get<UIManagerSignal.GetCurrentTopWindow>().AddListener(OnGetCurrentTopWindow);
            ManagersSignals.Get<UIManagerSignal.IsThisWindowOpened>().AddListener(OnIsThisWindowOpened);
            ManagersSignals.Get<UIManagerSignal.HideAllScreens>().AddListener(OnHideAllScreens);

            UIModuleSignals.Get<SplashSimpleWindowSignal.ChangeToNewScreen>().AddListener(OnNavigation);
            UIModuleSignals.Get<MainMenuSimpleWindowSignal.ChangeToNewScreen>().AddListener(OnNavigation);
            
            UIModuleSignals.Get<InteractionToolsPanelSignal.ChangeToNewScreen>().AddListener(OnNavigation);
            UIModuleSignals.Get<MenuPopUpWindowSignal.ChangeToNewScreen>().AddListener(OnNavigation);

            UIModuleSignals.Get<BasicPopUpWindowSignal.ChangeToNewScreen>().AddListener(OnNavigation);
            UIModuleSignals.Get<InfoPopUpWindowSignal.ChangeToNewScreen>().AddListener(OnNavigation);
        }

        private void OnDisable()
        {
            ManagersSignals.Get<UIManagerSignal.Setup>().RemoveListener(OnSetup);
            ManagersSignals.Get<UIManagerSignal.ChangeToNewScreen>().RemoveListener(OnNavigation);
            ManagersSignals.Get<UIManagerSignal.IsWindowOnTopOfAllWindows>().RemoveListener(OnIsWindowOnTopOfAllWindows);
            ManagersSignals.Get<UIManagerSignal.GetCurrentTopWindow>().RemoveListener(OnGetCurrentTopWindow);
            ManagersSignals.Get<UIManagerSignal.IsThisWindowOpened>().RemoveListener(OnIsThisWindowOpened);
            ManagersSignals.Get<UIManagerSignal.HideAllScreens>().RemoveListener(OnHideAllScreens);

            UIModuleSignals.Get<SplashSimpleWindowSignal.ChangeToNewScreen>().RemoveListener(OnNavigation);
            UIModuleSignals.Get<MainMenuSimpleWindowSignal.ChangeToNewScreen>().RemoveListener(OnNavigation);

            UIModuleSignals.Get<InteractionToolsPanelSignal.ChangeToNewScreen>().RemoveListener(OnNavigation);

            UIModuleSignals.Get<BasicPopUpWindowSignal.ChangeToNewScreen>().RemoveListener(OnNavigation);
            UIModuleSignals.Get<InfoPopUpWindowSignal.ChangeToNewScreen>().RemoveListener(OnNavigation);
            UIModuleSignals.Get<MenuPopUpWindowSignal.ChangeToNewScreen>().RemoveListener(OnNavigation);
        }

        private async Task<(Exception, bool)> OnSetup(BuildType buildType, TargetResolution tarRes, bool autoStartUI)
        {
            _buildType = buildType;
            _tarRes = tarRes;

            UISettings uISettings = null;

            if (_buildType == BuildType.Production)
            {
                if (_tarRes == TargetResolution.Tablet)
                {
                    uISettings = _defaultUISettings;
                    uIFrame = _defaultUISettings.CreateUIInstance();
                }
                else if (_tarRes == TargetResolution.Phone)
                {
                    uISettings = _defaultUISettingsMob;
                    uIFrame = _defaultUISettingsMob.CreateUIInstance();
                }

                uIFrame.transform.SetParent(this.transform, false);
                if (autoStartUI)
                {
                    OnNavigation(UIManagerDataConv.ScreenIdsStrToEnum(uISettings.GetFirstScreenName()));
                    if (uISettings.SecondEntryIsPanel)
                    {
                        OnNavigation(ScreenIds.None, null, UIManagerDataConv.ScreenIdsStrToEnum(uISettings.GetSecondScreenName()));
                    }
                }
            }
            else
            {
                if (_uIFlow == UIFlow.Normal && _tarRes == TargetResolution.Tablet)
                {
                    uISettings = _defaultUISettings;
                    uIFrame = _defaultUISettings.CreateUIInstance();
                }
                else if (_uIFlow == UIFlow.Normal && _tarRes == TargetResolution.Phone)
                {
                    uISettings = _defaultUISettingsMob;
                    uIFrame = _defaultUISettingsMob.CreateUIInstance();
                }
                else if (_uIFlow == UIFlow.Specific && _tarRes == TargetResolution.Tablet)
                {
                    uISettings = _testingUISettings;
                    uIFrame = _testingUISettings.CreateUIInstance();
                }
                else if (_uIFlow == UIFlow.Specific && _tarRes == TargetResolution.Phone)
                {
                    uISettings = _testingUISettingsMob;
                    uIFrame = _testingUISettingsMob.CreateUIInstance();
                }

                uIFrame.transform.SetParent(this.transform, false);
                if (autoStartUI)
                {
                    OnNavigation(UIManagerDataConv.ScreenIdsStrToEnum(uISettings.GetFirstScreenName()));
                    if (uISettings.SecondEntryIsPanel)
                    {
                        OnNavigation(ScreenIds.None, null, UIManagerDataConv.ScreenIdsStrToEnum(uISettings.GetSecondScreenName()));
                    }
                }
            }

            return (null, true);
        }

        private Void OnNavigation(ScreenIds windowId, WindowProperties windowProperties = null, ScreenIds panelIdToShow = ScreenIds.None, PanelProperties panelProperties = null, ScreenIds panelIdToHide = ScreenIds.None)
        {
            if (windowId != ScreenIds.None)
            {
                uIFrame.OpenWindow(windowId.ToString(), windowProperties);
            }

            if (panelIdToShow != ScreenIds.None)
            {
                uIFrame.ShowPanel(panelIdToShow.ToString(), panelProperties);
            }

            if (panelIdToHide != ScreenIds.None)
            {
                uIFrame.HidePanel(panelIdToHide.ToString());
            }

            return null;
        }

        private bool OnIsThisWindowOpened(ScreenIds screenIds)
        {
            return uIFrame.IsThisWindowOpened(screenIds);
        }

        private bool OnIsWindowOnTopOfAllWindows(IWindowController arg)
        {
            return uIFrame.IsThisWindowTopOfWindows(arg);
        }

        private ScreenIds OnGetCurrentTopWindow()
        {
            return UIManagerDataConv.ScreenIdsStrToEnum(uIFrame.GetTopOfWindows().ScreenId);
        }

        private void OnHideAllScreens()
        {
            uIFrame.HideAll(true);
        }
    }
}