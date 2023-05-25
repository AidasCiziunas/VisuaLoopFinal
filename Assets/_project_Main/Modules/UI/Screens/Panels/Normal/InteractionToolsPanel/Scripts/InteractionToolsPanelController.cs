using Doozy.Runtime.Reactor.Animators;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SyedAli.Main.UIModule.InteractionToolsPanel
{
    public class InteractionToolsPanelController : APanelController<InteractionToolsPanelProperties>
    {
        [SerializeField] PanelDrawerButtonWidgetController _panelDrawerBtnWidCtrl;

        [Space(10)]
        [SerializeField] ThreeDButtonWidgetController _threeDBtnWidCtrl;
        [SerializeField] MoveButtonWidgetController _moveBtnWidCtrl;
        [SerializeField] ZoomButtonWidgetController _zoomBtnWidCtrl;
        [SerializeField] InfoButtonWidgetController _infoBtnWidCtrl;

        [Space(10)]
        [SerializeField] UIAnimator _openPanelAnim;
        [SerializeField] UIAnimator _closePanelAnim;

        private InteractionType _currentInteractionType;
        private InteractionPanelState _currentPanelState = InteractionPanelState.Open;

        protected override void AddListeners()
        {
            _panelDrawerBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickPanelDrawer;
            _threeDBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickThreeD;
            _moveBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickMove;
            _zoomBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickZoom;
            _infoBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickInfo;
        }

        protected override void RemoveListeners()
        {
            _panelDrawerBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickPanelDrawer;
            _threeDBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickThreeD;
            _moveBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickMove;
            _zoomBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickZoom;
            _infoBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickInfo;
        }

        protected override void OnPropertiesSet()
        {
            base.OnPropertiesSet();

            _openPanelAnim.animation.OnPlayCallback.AddListener(OnPlayAnim);
            _openPanelAnim.animation.OnFinishCallback.AddListener(OnFinishAnim);

            _closePanelAnim.animation.OnPlayCallback.AddListener(OnPlayAnim);
            _closePanelAnim.animation.OnFinishCallback.AddListener(OnFinishAnim);

            _threeDBtnWidCtrl.SetData(new ThreeDButtonWidgetProperties(ThreeDButtonState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.ThreeD), true));
            _moveBtnWidCtrl.SetData(new MoveButtonWidgetProperties(MoveButtonState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Move), true));
            _zoomBtnWidCtrl.SetData(new ZoomButtonWidgetProperties(ZoomButtonState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Zoom), true));
            _infoBtnWidCtrl.SetData(new InfoButtonWidgetProperties(InfoButtonState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Info), true));

            SelectButton(InteractionType.ThreeD);
        }

        protected override void WhileHiding()
        {
            base.WhileHiding();

            _openPanelAnim.animation.OnPlayCallback.RemoveListener(OnPlayAnim);
            _openPanelAnim.animation.OnFinishCallback.RemoveListener(OnFinishAnim);

            _closePanelAnim.animation.OnPlayCallback.RemoveListener(OnPlayAnim);
            _closePanelAnim.animation.OnFinishCallback.RemoveListener(OnFinishAnim);
        }

        private void OnClickPanelDrawer(object sender, EventArgs e)
        {
            switch (_currentPanelState)
            {
                case InteractionPanelState.Open:
                    _closePanelAnim.Play();
                    break;
                case InteractionPanelState.Close:
                    _openPanelAnim.Play();
                    break;
            }
        }

        private void OnClickThreeD(object sender, EventArgs e)
        {
            SelectButton(InteractionType.ThreeD);
        }

        private void OnClickMove(object sender, EventArgs e)
        {
            SelectButton(InteractionType.Move);
        }

        private void OnClickZoom(object sender, EventArgs e)
        {
            SelectButton(InteractionType.Zoom);
        }

        private void OnClickInfo(object sender, EventArgs e)
        {
            SelectButton(InteractionType.Info);

            InfoPopUpWindowProperties props = new InfoPopUpWindowProperties(() =>
            {
                SelectButton(InteractionType.None);
            });

            TargetResolution tarRes = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();

            if (tarRes == TargetResolution.Tablet)
                UIModuleSignals.Get<InteractionToolsPanelSignal.ChangeToNewScreen>().Dispatch(ScreenIds.InfoPopUpWindow, props, ScreenIds.None, null, ScreenIds.None);
            else
                UIModuleSignals.Get<InteractionToolsPanelSignal.ChangeToNewScreen>().Dispatch(ScreenIds.InfoPopUpWindow_Mob, props, ScreenIds.None, null, ScreenIds.None);
        }

        private void OnPlayAnim()
        {
            _panelDrawerBtnWidCtrl.Refresh(new PanelDrawerButtonWidgetProperties(false));

            _currentPanelState = _currentPanelState == InteractionPanelState.Open ? InteractionPanelState.Close : InteractionPanelState.Open;

            switch (_currentPanelState)
            {
                case InteractionPanelState.Open:
                    _threeDBtnWidCtrl.Refresh(new ThreeDButtonWidgetProperties(ThreeDButtonState.Normal, _threeDBtnWidCtrl.Props.MainText, true));
                    _zoomBtnWidCtrl.Refresh(new ZoomButtonWidgetProperties(ZoomButtonState.Normal, _zoomBtnWidCtrl.Props.MainText, true));
                    _moveBtnWidCtrl.Refresh(new MoveButtonWidgetProperties(MoveButtonState.Normal, _moveBtnWidCtrl.Props.MainText, true));
                    _infoBtnWidCtrl.Refresh(new InfoButtonWidgetProperties(InfoButtonState.Normal, _infoBtnWidCtrl.Props.MainText, true));
                    SelectButton(_currentInteractionType);
                    break;
                case InteractionPanelState.Close:
                    _threeDBtnWidCtrl.Refresh(new ThreeDButtonWidgetProperties(ThreeDButtonState.Disabled, _threeDBtnWidCtrl.Props.MainText, false));
                    _zoomBtnWidCtrl.Refresh(new ZoomButtonWidgetProperties(ZoomButtonState.Disabled, _zoomBtnWidCtrl.Props.MainText, false));
                    _moveBtnWidCtrl.Refresh(new MoveButtonWidgetProperties(MoveButtonState.Disabled, _moveBtnWidCtrl.Props.MainText, false));
                    _infoBtnWidCtrl.Refresh(new InfoButtonWidgetProperties(InfoButtonState.Disabled, _infoBtnWidCtrl.Props.MainText, false));
                    break;
            }
        }

        private void OnFinishAnim()
        {
            _panelDrawerBtnWidCtrl.Refresh(new PanelDrawerButtonWidgetProperties(true));
        }

        private void SelectButton(InteractionType interactionMode)
        {
            switch(interactionMode)
            {
                case InteractionType.None:
                    _threeDBtnWidCtrl.Refresh(new ThreeDButtonWidgetProperties(ThreeDButtonState.Normal, _threeDBtnWidCtrl.Props.MainText, _threeDBtnWidCtrl.Props.Visibility));
                    _zoomBtnWidCtrl.Refresh(new ZoomButtonWidgetProperties(ZoomButtonState.Normal, _zoomBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _moveBtnWidCtrl.Refresh(new MoveButtonWidgetProperties(MoveButtonState.Normal, _moveBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _infoBtnWidCtrl.Refresh(new InfoButtonWidgetProperties(InfoButtonState.Normal, _infoBtnWidCtrl.Props.MainText, _infoBtnWidCtrl.Props.Visibility));
                    break;
                case InteractionType.ThreeD:
                    _threeDBtnWidCtrl.Refresh(new ThreeDButtonWidgetProperties(ThreeDButtonState.Pressed, _threeDBtnWidCtrl.Props.MainText, _threeDBtnWidCtrl.Props.Visibility));
                    _zoomBtnWidCtrl.Refresh(new ZoomButtonWidgetProperties(ZoomButtonState.Normal, _zoomBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _moveBtnWidCtrl.Refresh(new MoveButtonWidgetProperties(MoveButtonState.Normal, _moveBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _infoBtnWidCtrl.Refresh(new InfoButtonWidgetProperties(InfoButtonState.Normal, _infoBtnWidCtrl.Props.MainText, _infoBtnWidCtrl.Props.Visibility));
                    break;
                case InteractionType.Zoom:
                    _threeDBtnWidCtrl.Refresh(new ThreeDButtonWidgetProperties(ThreeDButtonState.Normal, _threeDBtnWidCtrl.Props.MainText, _threeDBtnWidCtrl.Props.Visibility));
                    _zoomBtnWidCtrl.Refresh(new ZoomButtonWidgetProperties(ZoomButtonState.Pressed, _zoomBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _moveBtnWidCtrl.Refresh(new MoveButtonWidgetProperties(MoveButtonState.Normal, _moveBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _infoBtnWidCtrl.Refresh(new InfoButtonWidgetProperties(InfoButtonState.Normal, _infoBtnWidCtrl.Props.MainText, _infoBtnWidCtrl.Props.Visibility));
                    break;
                case InteractionType.Move:
                    _threeDBtnWidCtrl.Refresh(new ThreeDButtonWidgetProperties(ThreeDButtonState.Normal, _threeDBtnWidCtrl.Props.MainText, _threeDBtnWidCtrl.Props.Visibility));
                    _zoomBtnWidCtrl.Refresh(new ZoomButtonWidgetProperties(ZoomButtonState.Normal, _zoomBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _moveBtnWidCtrl.Refresh(new MoveButtonWidgetProperties(MoveButtonState.Pressed, _moveBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _infoBtnWidCtrl.Refresh(new InfoButtonWidgetProperties(InfoButtonState.Normal, _infoBtnWidCtrl.Props.MainText, _infoBtnWidCtrl.Props.Visibility));
                    break;
                case InteractionType.Info:
                    _threeDBtnWidCtrl.Refresh(new ThreeDButtonWidgetProperties(ThreeDButtonState.Normal, _threeDBtnWidCtrl.Props.MainText, _threeDBtnWidCtrl.Props.Visibility));
                    _zoomBtnWidCtrl.Refresh(new ZoomButtonWidgetProperties(ZoomButtonState.Normal, _zoomBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _moveBtnWidCtrl.Refresh(new MoveButtonWidgetProperties(MoveButtonState.Normal, _moveBtnWidCtrl.Props.MainText, _zoomBtnWidCtrl.Props.Visibility));
                    _infoBtnWidCtrl.Refresh(new InfoButtonWidgetProperties(InfoButtonState.Pressed, _infoBtnWidCtrl.Props.MainText, _infoBtnWidCtrl.Props.Visibility));
                    break;
            }

            _currentInteractionType = interactionMode;

            UIModuleSignals.Get<InteractionToolsPanelSignal.PanelInteractionClicked>().Dispatch(_currentInteractionType);
        }
    }
}
