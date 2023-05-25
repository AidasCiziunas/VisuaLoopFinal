using Doozy.Runtime.Reactor.Animators;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static SyedAli.Main.InfoPopUpWindowSignal;

namespace SyedAli.Main.UIModule.MainMenuSimpleWindow
{
    public class MainMenuSimpleWindowController : AWindowController<MainMenuSimpleWindowProperties>
    {
        [SerializeField] Transform _rootContentT;
        [Space(10)]

        [SerializeField] TextMeshProUGUI _mainHeadingTxt;
        [SerializeField] MenuButtonWidgetController _menuBtnWidCtrl;

        [Space(10)]
        [SerializeField] GameObject _pipeTilePrfb;
        [SerializeField] Transform _pipeTileParentT;
        [SerializeField] List<PipeData> _pipeDataLs;

        [Space(10)]
        [SerializeField] DrawerButtonWidgetController _drawerBtnWidCtrl;
        [SerializeField] TextMeshProUGUI _nameTxt;

        [Space(10)]
        [SerializeField] Transform _lowerSectionT;
        [SerializeField] Vector2Animator _lowerFullModeSzeDltaAnim;
        [SerializeField] Vector2Animator _lowerSmallModeSzeDltaAnim;
        [SerializeField] Vector2Animator _lowerFullModeYPosAnim;
        [SerializeField] Vector2Animator _lowerSmallModeYPosAnim;

        [Space(10)]
        [SerializeField] GameObject _zoomSldrGo;
        [SerializeField] ZoomSliderWidgetController _zoomSldrWidCtrl;

        [Space(10)]
        [SerializeField] GameObject _moveLeftRightGo;
        [SerializeField] MoveLeftRightVisualWidgetController _moveLeftRightVisWidCtrl;
        [SerializeField] GameObject _moveUpDownGo;
        [SerializeField] MoveUpDownVisualWidgetController _moveUpDownVisWidCtrl;
        [SerializeField] GameObject _rotateGo;
        [SerializeField] RotateVisualWidgetController _rotateVisWidCtrl;

        [Space(10)]
        [SerializeField] ModelCustomWidgetController _modelCustWidCtrl;
        
        [Space(10)]
        [SerializeField] AxisCustomWidgetController _axisCustWidCtrl;

        [Space(10)]
        [SerializeField] ResetButtonWidgetController _resetBtnWidCtrl;

        [Space(10)]
        [SerializeField] RectTransform _lowerRectT;

        [Space(10)]
        [SerializeField] Transform _topBarT;

        [Space(10)]
        [SerializeField] ZoomInButtonWidgetController _zoomInBtnWidCtrl;
        [SerializeField] ZoomOutButtonWidgetController _zoomOutBtnWidCtrl;

        private List<PipeTileWidgetController> _pipeTileWidCtrlLs = new List<PipeTileWidgetController>();
        private MainMenuMode _currentMainMenuMode;
        private List<InstantiatedPipeData> _cachePipeDataLs = new List<InstantiatedPipeData>();
        private PipeType _selectedPipeType;

        private bool _propsAlreadySetup = false;
        private float _lowerRectTHeightInit;

        private void Start()
        {
            _lowerRectTHeightInit = _lowerRectT.sizeDelta.y;
        }

        protected override void AddListeners()
        {
            base.AddListeners();

            _menuBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickMenuBtn;
            _drawerBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickDrawerBtn;

            _axisCustWidCtrl.YBtnPointerClicked_EventHandler += OnClickYAxisBtn;
            _axisCustWidCtrl.ZBtnPointerClicked_EventHandler += OnClickZAxisBtn;
            _axisCustWidCtrl.XBtnPointerClicked_EventHandler += OnClickXAxisBtn;

            _zoomSldrWidCtrl.SliderValChanged_EventHandler += OnValChangedSlider;

            _modelCustWidCtrl.ModelMoveBehaviour_EventHandler += OnModelMoveBehaviour;
            _modelCustWidCtrl.ModelZoomBehaviour_EventHandler += OnModelZoomBehaviour;

            _resetBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickResetBtn;

            if (_zoomInBtnWidCtrl != null)
                _zoomInBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickZoomInBtn;
    
            if (_zoomOutBtnWidCtrl != null)
                _zoomOutBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickZoomOutBtn;

            UIModuleSignals.Get<InteractionToolsPanelSignal.PanelInteractionClicked>().AddListener(OnInteractionPanelButton);
            UIModuleSignals.Get<InfoPopUpWindowSignal.InfoPopUpClosed>().AddListener(OnInfoPopUpClosed);
            UIModuleSignals.Get<MainMenuSimpleWindowSignal.GetSelectedPipeType>().AddListener(OnGetSelectedPipeType);
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();

            _menuBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickMenuBtn;
            _drawerBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickDrawerBtn;

            _axisCustWidCtrl.YBtnPointerClicked_EventHandler -= OnClickYAxisBtn;
            _axisCustWidCtrl.ZBtnPointerClicked_EventHandler -= OnClickZAxisBtn;
            _axisCustWidCtrl.XBtnPointerClicked_EventHandler -= OnClickXAxisBtn;

            _zoomSldrWidCtrl.SliderValChanged_EventHandler -= OnValChangedSlider;

            _modelCustWidCtrl.ModelMoveBehaviour_EventHandler -= OnModelMoveBehaviour;
            _modelCustWidCtrl.ModelZoomBehaviour_EventHandler -= OnModelZoomBehaviour;

            _resetBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickResetBtn;

            if (_zoomInBtnWidCtrl != null)
                _zoomInBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickZoomInBtn;

            if (_zoomOutBtnWidCtrl != null)
                _zoomOutBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickZoomOutBtn;

            UIModuleSignals.Get<InteractionToolsPanelSignal.PanelInteractionClicked>().RemoveListener(OnInteractionPanelButton);
            UIModuleSignals.Get<InfoPopUpWindowSignal.InfoPopUpClosed>().RemoveListener(OnInfoPopUpClosed);
            UIModuleSignals.Get<MainMenuSimpleWindowSignal.GetSelectedPipeType>().RemoveListener(OnGetSelectedPipeType);
        }

        protected override void OnPropertiesSet()
        {
            if (_propsAlreadySetup)
                return;

            base.OnPropertiesSet();

            _mainHeadingTxt.text = LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Products);

            SetupTiles();

            _modelCustWidCtrl.SetData(new ModelCustomWidgetProperties(ModelWidgetMode.ShowOnly, InstantChangeAxis.None, InstantiateOrGetFromCachePipe(PipeType.UStyleLoop), 0.0f, false));
            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.ThreeD, InstantChangeAxis.InstZAxis, _modelCustWidCtrl.Props.ModelGo, _modelCustWidCtrl.Props.Zoom, true));
            SelectButton(PipeType.UStyleLoop);
            StateModuleSignals.Get<GameStateSignal.UpdateSelectedPipeType>().Dispatch(PipeType.UStyleLoop);

            UpdatePipeInfo();

            _drawerBtnWidCtrl.SetData(new DrawerButtonWidgetProperties(DrawerBtnState.Up));
            _currentMainMenuMode = MainMenuMode.Small;

            _zoomSldrWidCtrl.SetData(new ZoomSliderWidgetProperties(0, 0, 150.0f));

            _moveLeftRightVisWidCtrl.SetData(new MoveLeftRightVisualWidgetProperties(LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.MoveLeftRight)));
            _moveUpDownVisWidCtrl.SetData(new MoveUpDownVisualWidgetProperties(LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.MoveUpDown)));
            _rotateVisWidCtrl.SetData(new RotateVisualWidgetProperties(LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Rotate)));

            _resetBtnWidCtrl.SetData(new ResetButtonWidgetProperties(ResetButtonState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Reset), true));

            _menuBtnWidCtrl.SetData(new MenuButtonWidgetProperties(true));

            _propsAlreadySetup = true;
        }

        private PipeType OnGetSelectedPipeType()
        {
            return _selectedPipeType;
        }

        private void OnClickMenuBtn(object sender, EventArgs e)
        {
            _modelCustWidCtrl.Visibility(false);

            UnityAction<Transform> topBarAct = (t) =>
            {
                _resetBtnWidCtrl.SetData(new ResetButtonWidgetProperties(_resetBtnWidCtrl.Props.InteractionToolButtonState, _resetBtnWidCtrl.Props.MainText, false));
                _menuBtnWidCtrl.Refresh(new MenuButtonWidgetProperties(false));
                _topBarT.SetParent(t, false);
                _topBarT.SetAsFirstSibling();
            };

            UnityAction act = () =>
            {
                _modelCustWidCtrl.Visibility(true);

                _resetBtnWidCtrl.SetData(new ResetButtonWidgetProperties(_resetBtnWidCtrl.Props.InteractionToolButtonState, _resetBtnWidCtrl.Props.MainText, true));
                _menuBtnWidCtrl.Refresh(new MenuButtonWidgetProperties(true));
                _topBarT.SetParent(_rootContentT, false);
                _topBarT.SetSiblingIndex(1);
            };

            TargetResolution tarRes = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();
            if (tarRes == TargetResolution.Tablet)
                UIModuleSignals.Get<MainMenuSimpleWindowSignal.ChangeToNewScreen>().Dispatch(ScreenIds.MenuPopUpWindow, new MenuPopUpWindowProperties(act), ScreenIds.None, null, ScreenIds.None);
            else if (tarRes == TargetResolution.Phone)
                UIModuleSignals.Get<MainMenuSimpleWindowSignal.ChangeToNewScreen>().Dispatch(ScreenIds.MenuPopUpWindow_Mob, new MenuPopUpWindowProperties(act, topBarAct), ScreenIds.None, null, ScreenIds.None);
        }

        private void OnClickDrawerBtn(object sender, EventArgs e)
        {
            SelectMainMenuMode(_currentMainMenuMode == MainMenuMode.Full ? MainMenuMode.Small : MainMenuMode.Full);
        }

        private void OnClickTile(object sender, PipeTileBtnEventArgs e)
        {
            ModelWidgetMode oldMode = _modelCustWidCtrl.Props.ModelWidMode;
            _zoomSldrWidCtrl.Refresh(new ZoomSliderWidgetProperties(0, 0, 150));
            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.ShowOnly, InstantChangeAxis.None, InstantiateOrGetFromCachePipe(e.Props.Id), _zoomSldrWidCtrl.SliderVal, false));

            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(oldMode, InstantChangeAxis.InstZAxis, InstantiateOrGetFromCachePipe(e.Props.Id), _zoomSldrWidCtrl.SliderVal, true));

            SelectButton(e.Props.Id);

            StateModuleSignals.Get<GameStateSignal.UpdateSelectedPipeType>().Dispatch(e.Props.Id);
        }

        private void OnClickYAxisBtn(object sender, AxisBtnEventArgs e)
        {
            ModelWidgetMode mode = _modelCustWidCtrl.Props.ModelWidMode;
            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(mode, InstantChangeAxis.InstYAxis, _modelCustWidCtrl.Props.ModelGo, 0.0f, 
                e.AxisSide == AxisSide.Pos ? true : false));
        }

        private void OnClickZAxisBtn(object sender, AxisBtnEventArgs e)
        {
            ModelWidgetMode mode = _modelCustWidCtrl.Props.ModelWidMode;
            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(mode, InstantChangeAxis.InstZAxis, _modelCustWidCtrl.Props.ModelGo, 0.0f,
                e.AxisSide == AxisSide.Pos ? true : false));
        }

        private void OnClickXAxisBtn(object sender, AxisBtnEventArgs e)
        {
            ModelWidgetMode mode = _modelCustWidCtrl.Props.ModelWidMode;
            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(mode, InstantChangeAxis.InstXAxis, _modelCustWidCtrl.Props.ModelGo, 0.0f,
                e.AxisSide == AxisSide.Pos ? true : false));
        }

        private void OnClickResetBtn(object sender, EventArgs e)
        {
            switch(_modelCustWidCtrl.Props.ModelWidMode)
            {
                case ModelWidgetMode.ThreeD:
                    _modelCustWidCtrl.ResetModel(ResetType.ThreeD);
                    break;
                case ModelWidgetMode.Move:
                    _modelCustWidCtrl.ResetModel(ResetType.Move);
                    break;
                case ModelWidgetMode.Zoom:
                    TargetResolution tarRes = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();
                    if (tarRes == TargetResolution.Phone)
                    {
                        _modelCustWidCtrl.ResetModel(ResetType.Zoom, 0.0f);
                    }
                    else
                        _modelCustWidCtrl.ResetModel(ResetType.Zoom, _zoomSldrWidCtrl.SliderVal);

                    _zoomSldrWidCtrl.Refresh(new ZoomSliderWidgetProperties(0, 0, 150));
                    break;
            }
        }

        private void OnClickZoomInBtn(object sender, EventArgs e)
        {
            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.Zoom, InstantChangeAxis.None, _modelCustWidCtrl.Props.ModelGo, _modelCustWidCtrl.Props.Zoom + 0.05f, false));
        }

        private void OnClickZoomOutBtn(object sender, EventArgs e)
        {
            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.Zoom, InstantChangeAxis.None, _modelCustWidCtrl.Props.ModelGo, _modelCustWidCtrl.Props.Zoom - 0.05f, false));
        }

        private void OnValChangedSlider(object sender, ZoomSliderChangeEventArgs e)
        {
            _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.Zoom, InstantChangeAxis.None, _modelCustWidCtrl.Props.ModelGo, e.SliderVal, false));
        }

        private Void OnInteractionPanelButton(InteractionType interactionType)
        {
            TargetResolution targetResolution = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();

            switch (interactionType)
            {
                case InteractionType.ThreeD:
                    _moveLeftRightGo.SetActive(false);
                    _moveUpDownGo.SetActive(false);
                    _rotateGo.SetActive(false);
                    _zoomSldrGo.SetActive(false);

                    _axisCustWidCtrl.Visibility(true);

                    if (targetResolution == TargetResolution.Phone)
                    {
                        _zoomInBtnWidCtrl.Visibility(false);
                        _zoomOutBtnWidCtrl.Visibility(false);
                    }

                    _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.ThreeD, InstantChangeAxis.InstZAxis, _modelCustWidCtrl.Props.ModelGo, _modelCustWidCtrl.Props.Zoom, true));
                    break;
                case InteractionType.Move:
                    _moveLeftRightGo.SetActive(true);
                    _moveUpDownGo.SetActive(true);
                    _rotateGo.SetActive(true);
                    _zoomSldrGo.SetActive(false);

                    _axisCustWidCtrl.Visibility(false);

                    if (targetResolution == TargetResolution.Phone)
                    {
                        _zoomInBtnWidCtrl.Visibility(false);
                        _zoomOutBtnWidCtrl.Visibility(false);
                    }

                    _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.Move, InstantChangeAxis.None, _modelCustWidCtrl.Props.ModelGo, _modelCustWidCtrl.Props.Zoom, false));
                    break;
                case InteractionType.Zoom:
                    _moveLeftRightGo.SetActive(false);
                    _moveUpDownGo.SetActive(false);
                    _rotateGo.SetActive(false);

                    _axisCustWidCtrl.Visibility(false);

                    if (targetResolution == TargetResolution.Phone)
                    {
                        _zoomInBtnWidCtrl.Visibility(true);
                        _zoomOutBtnWidCtrl.Visibility(true);
                        _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.Zoom, InstantChangeAxis.None, _modelCustWidCtrl.Props.ModelGo, _modelCustWidCtrl.Props.Zoom, false));
                    }
                    else
                    {
                        _zoomSldrGo.SetActive(true);
                        _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.Zoom, InstantChangeAxis.None, _modelCustWidCtrl.Props.ModelGo, _zoomSldrWidCtrl.SliderVal, false));
                    }

                    break;
                case InteractionType.Info:
                    _moveLeftRightGo.SetActive(false);
                    _moveUpDownGo.SetActive(false);
                    _rotateGo.SetActive(false);
                    _zoomSldrGo.SetActive(false);

                    if (targetResolution == TargetResolution.Phone)
                    {
                        _zoomInBtnWidCtrl.Visibility(false);
                        _zoomOutBtnWidCtrl.Visibility(false);
                    }

                    _modelCustWidCtrl.Refresh(new ModelCustomWidgetProperties(ModelWidgetMode.Info, InstantChangeAxis.None, _modelCustWidCtrl.Props.ModelGo, _modelCustWidCtrl.Props.Zoom, false));
                    _modelCustWidCtrl.Visibility(false);
                    break;
            }

            return null;
        }

        private void OnInfoPopUpClosed()
        {
            _modelCustWidCtrl.Visibility(true);
        }

        private void OnModelMoveBehaviour(object sender, ModelMoveBehaviourEventArgs e)
        {
            if (e.BehaviourStatus)
            {
                _moveUpDownGo.SetActive(false);
                _moveLeftRightGo.SetActive(false);
                _rotateGo.SetActive(false);
            }
            else
            {
                _moveUpDownGo.SetActive(true);
                _moveLeftRightGo.SetActive(true);
                _rotateGo.SetActive(true);
            }
        }

        private void OnModelZoomBehaviour(object sender, ModelZoomBehaviourEventArgs e)
        {
            TargetResolution tarRes = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();
            if (tarRes == TargetResolution.Phone)
            {
                switch (e.ZoomType)
                {
                    case ZoomType.PinchIn:
                        OnClickZoomInBtn(this, new EventArgs());
                        break;
                    case ZoomType.PinchOut:
                        OnClickZoomOutBtn(this, new EventArgs());
                        break;
                }
            }
            else if (tarRes == TargetResolution.Tablet)
            {
                switch (e.ZoomType)
                {
                    case ZoomType.PinchIn:
                        _zoomSldrWidCtrl.Refresh(new ZoomSliderWidgetProperties(Int32.Parse(_zoomSldrWidCtrl.SliderValStr) + 5, _zoomSldrWidCtrl.Props.MinVal, _zoomSldrWidCtrl.Props.MaxVal));
                        break;
                    case ZoomType.PinchOut:
                        _zoomSldrWidCtrl.Refresh(new ZoomSliderWidgetProperties(Int32.Parse(_zoomSldrWidCtrl.SliderValStr) - 5, _zoomSldrWidCtrl.Props.MinVal, _zoomSldrWidCtrl.Props.MaxVal));
                        break;
                }
            }
        }

        private void UpdatePipeInfo()
        {
            foreach(var entry in _pipeDataLs)
            {
                StateModuleSignals.Get<GameStateSignal.UpdatePipeInfoData>().Dispatch(entry.PipeType, entry.PipeInfoTxtAst);
            }
        }

        private void SetupTiles()
        {
            if (_pipeTileWidCtrlLs.Count == 0)
            {
                foreach (var entry in _pipeDataLs)
                {
                    PipeTileWidgetProperties props = new PipeTileWidgetProperties(entry.PipeType, PipeTileBtnState.Normal, entry.PipeSprt, entry.PipeName);
                    _pipeTileWidCtrlLs.Add(CreateTile(props));
                }
            }
        }

        private PipeTileWidgetController CreateTile(PipeTileWidgetProperties props)
        {
            GameObject tileGo = Instantiate(_pipeTilePrfb, new Vector3(0, 0, 0), Quaternion.identity);
            tileGo.transform.SetParent(_pipeTileParentT, false);

            PipeTileWidgetController ctrl = tileGo.GetComponent<PipeTileWidgetController>();
            ctrl.SetData(props);

            ctrl.MainBtnPointerClicked_EventHandler += OnClickTile;

            return ctrl;
        }

        private void SelectButton(PipeType pipeType)
        {
            foreach (var entry in _pipeTileWidCtrlLs)
            {
                if (entry.Props.Id == pipeType)
                {
                    entry.Refresh(new PipeTileWidgetProperties(entry.Props.Id, PipeTileBtnState.Pressed, entry.Props.Image, entry.Props.Name));
                    _nameTxt.text = entry.Props.Name;
                    _selectedPipeType = pipeType;
                }
                else
                {
                    entry.Refresh(new PipeTileWidgetProperties(entry.Props.Id, PipeTileBtnState.Normal, entry.Props.Image, entry.Props.Name));
                }
            }
        }

        private void SelectMainMenuMode(MainMenuMode mainMenuMode)
        {
            TargetResolution tarRes = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();

            switch (mainMenuMode)
            {
                case MainMenuMode.Full:
                    if (tarRes == TargetResolution.Phone)
                    {
                        _lowerFullModeYPosAnim.Play();
                        //_lowerFullModeSzeDltaAnim.Play();
                    }
                    else
                        _lowerFullModeSzeDltaAnim.Play();

                    _drawerBtnWidCtrl.Refresh(new DrawerButtonWidgetProperties(DrawerBtnState.Down));
                    _pipeTileParentT.gameObject.SetActive(false);
                    OnDrawerOpenAnimFinished(DrawerBehaviourType.Opened);
                    break;
                case MainMenuMode.Small:
                    if (tarRes == TargetResolution.Phone)
                    {
                        _lowerSmallModeYPosAnim.Play();
                        //_lowerSmallModeSzeDltaAnim.Play();
                    }
                    else
                        _lowerSmallModeSzeDltaAnim.Play();

                    _drawerBtnWidCtrl.Refresh(new DrawerButtonWidgetProperties(DrawerBtnState.Up));
                    _pipeTileParentT.gameObject.SetActive(true);
                    OnDrawerCloseAnimFinished(DrawerBehaviourType.Closed);
                    break;
            }

            _currentMainMenuMode = mainMenuMode;
        }

        private void OnDrawerOpenAnimFinished(DrawerBehaviourType drawerBehType)
        {
            float height = _lowerRectT.sizeDelta.y - _lowerRectTHeightInit;
            _lowerRectTHeightInit = _lowerRectT.sizeDelta.y;
            UIModuleSignals.Get<MainMenuSimpleWindowSignal.DrawerInteraction>().Dispatch(drawerBehType, Mathf.Abs(height));
        }

        private void OnDrawerCloseAnimFinished(DrawerBehaviourType drawerBehType)
        {
            float height = _lowerRectT.sizeDelta.y - _lowerRectTHeightInit;
            _lowerRectTHeightInit = _lowerRectT.sizeDelta.y;
            UIModuleSignals.Get<MainMenuSimpleWindowSignal.DrawerInteraction>().Dispatch(drawerBehType, Mathf.Abs(height));
        }

        private GameObject InstantiateOrGetFromCachePipe(PipeType pipeType)
        {
            if (_cachePipeDataLs.Exists(x => x.PipeType == pipeType))
            {
                return _cachePipeDataLs.Find(x => x.PipeType == pipeType).PipeGo;
            }
            else
            {
                GameObject pipePrfb = _pipeDataLs.Find(x => x.PipeType == pipeType).PipePrfb;
                GameObject pipeGo = Instantiate(pipePrfb, new Vector3(0, 0, 0), Quaternion.identity);
                _cachePipeDataLs.Add(new InstantiatedPipeData(pipeType, pipeGo));
                return pipeGo;
            }
        }
    }
}
