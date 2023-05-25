using Newtonsoft.Json;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.InfoPopUpWindow
{
    public class InfoPopUpWindowController : AWindowController<InfoPopUpWindowProperties>
    {
        [SerializeField] CrossButtonWidgetController _crossBtnWidCtrl;
        [SerializeField] TextMeshProUGUI _mainHeadingTxt;

        [Space(10)]
        [SerializeField] List<TabularBtnData> _tabularBtnDataLs;

        [Space(10)]
        [SerializeField] GameObject _infoTilePrfb;
        [SerializeField] Transform _infoTileParentT;

        [Space(10)]
        [SerializeField] VerticalLayoutGroup _vLG;
        [SerializeField] ContentSizeFitter _cSF;

        private PipeInfoData _pipeInfoData;
        private List<InfoDescriptionTileWidgetController> _infoDescTileWidCtrlLs = new List<InfoDescriptionTileWidgetController>();

        private void OnApplicationPause(bool status)
        {
            Debug.Log("Called : " + status);
            if (status == false)
            {
                foreach (var entry in _infoDescTileWidCtrlLs)
                {
                    entry.Refresh(new InfoDescriptionTileWidgetProperties(true, entry.Props.Id, entry.Props.HeadingText, entry.Props.DescriptionInfoLs));
                }
            }
        }

        protected override void AddListeners()
        {
            base.AddListeners();

            if (_crossBtnWidCtrl != null)
                _crossBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickCrossBtn;

            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Detail).Ctrl.MainBtnPointerClicked_EventHandler += OnClickDetailBtn;
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Brochures).Ctrl.MainBtnPointerClicked_EventHandler += OnClickBrochuresBtn;
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.BIMContent).Ctrl.MainBtnPointerClicked_EventHandler += OnClickBIMContentBtn;
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Submittals).Ctrl.MainBtnPointerClicked_EventHandler += OnClickSubmittalsBtn;
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();

            if (_crossBtnWidCtrl != null)
                _crossBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickCrossBtn;

            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Detail).Ctrl.MainBtnPointerClicked_EventHandler -= OnClickDetailBtn;
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Brochures).Ctrl.MainBtnPointerClicked_EventHandler -= OnClickBrochuresBtn;
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.BIMContent).Ctrl.MainBtnPointerClicked_EventHandler -= OnClickBIMContentBtn;
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Submittals).Ctrl.MainBtnPointerClicked_EventHandler -= OnClickSubmittalsBtn;

            foreach(var entry in _infoDescTileWidCtrlLs)
            {
                entry.LinkClicked_EventHandler -= OnClickLink;
            }
        }

        protected override void OnPropertiesSet()
        {
            base.OnPropertiesSet();

            TargetResolution tarRes = ManagersSignals.Get<AppManagerSignal.GetTargetDevice>().Dispatch();
            if (tarRes == TargetResolution.Tablet)
                _mainHeadingTxt.text = GetSentenceTokenAgainstPipeType();
            else
                _mainHeadingTxt.text = LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Info);

            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Detail).Ctrl.SetData(new TabularButtonWidgetProperties(TabularBtnState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Detail)));
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Brochures).Ctrl.SetData(new TabularButtonWidgetProperties(TabularBtnState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Brochures)));
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.BIMContent).Ctrl.SetData(new TabularButtonWidgetProperties(TabularBtnState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.BIMContent)));
            _tabularBtnDataLs.Find(x => x.Type == TabularBtnType.Submittals).Ctrl.SetData(new TabularButtonWidgetProperties(TabularBtnState.Normal, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Submittals)));

            PipeType selectedPipeType = StateModuleSignals.Get<GameStateSignal.GetSelectedPipeType>().Dispatch();
            TextAsset textAsset = StateModuleSignals.Get<GameStateSignal.GetPipeInfoData>().Dispatch(selectedPipeType);
            _pipeInfoData = JsonConvert.DeserializeObject<PipeInfoData>(textAsset.text);

            SelectButton(TabularBtnType.Detail);
        }

        private string GetSentenceTokenAgainstPipeType()
        {
            PipeType pipeType = UIModuleSignals.Get<MainMenuSimpleWindowSignal.GetSelectedPipeType>().Dispatch();

            switch(pipeType)
            {
                case PipeType.VStyleLoop:
                    return LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.VStyleLoop);
                case PipeType.UStyleLoop:
                    return LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.UStyleLoop);
                case PipeType.TriLoop:
                    return LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.TriLoop);
                case PipeType.TriFlexLoop:
                    return LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.TriFlexLoop);
                default:
                    return null;
            }
        }

        private void OnClickCrossBtn(object sender, EventArgs e)
        {
            Properties.OnPopUpCloseAct?.Invoke();
            UIModuleSignals.Get<InfoPopUpWindowSignal.InfoPopUpClosed>().Dispatch();
            UI_Close();
        }

        private void SelectButton(TabularBtnType tabularBtnType)
        {
            foreach(var entry in _tabularBtnDataLs)
            {
                if (entry.Type == tabularBtnType)
                {
                    entry.Ctrl.Refresh(new TabularButtonWidgetProperties(TabularBtnState.Selected, entry.Ctrl.Props.BtnText));
                    SetupTile(tabularBtnType);
                }
                else
                {
                    entry.Ctrl.Refresh(new TabularButtonWidgetProperties(TabularBtnState.Normal, entry.Ctrl.Props.BtnText));
                }
            }

            if (gameObject.activeSelf)
                StartCoroutine(WaitAndUdpateCo());
        }

        IEnumerator WaitAndUdpateCo()
        {
            yield return new WaitForEndOfFrame();

            _vLG.CalculateLayoutInputVertical();
            _vLG.SetLayoutVertical();

            _cSF.SetLayoutVertical();

            Canvas.ForceUpdateCanvases();

        }

        private void OnClickDetailBtn(object sender, EventArgs e)
        {
            SelectButton(TabularBtnType.Detail);
        }

        private void OnClickBrochuresBtn(object sender, EventArgs e)
        {
            SelectButton(TabularBtnType.Brochures);
        }

        private void OnClickBIMContentBtn(object sender, EventArgs e)
        {
            SelectButton(TabularBtnType.BIMContent);
        }

        private void OnClickSubmittalsBtn(object sender, EventArgs e)
        {
            SelectButton(TabularBtnType.Submittals);
        }

        private void OnClickLink(object sender, EventArgs e)
        {
            foreach(var entry in _infoDescTileWidCtrlLs)
            {
                entry.DirectRefresh(new InfoDescriptionTileWidgetProperties(false, entry.Props.Id, entry.Props.HeadingText, entry.Props.DescriptionInfoLs));
            }
        }

        private void SetupTile(TabularBtnType tabularBtnType)
        {
            List<PipeInfoTileData> pipeInfoTileDataLs = new List<PipeInfoTileData>();

            switch(tabularBtnType)
            {
                case TabularBtnType.Detail:
                    pipeInfoTileDataLs = _pipeInfoData.Details;
                    break;
                case TabularBtnType.Brochures:
                    pipeInfoTileDataLs = _pipeInfoData.DocBrochers;
                    break;
                case TabularBtnType.BIMContent:
                    pipeInfoTileDataLs = _pipeInfoData.BIMContent;
                    break;
                case TabularBtnType.Submittals:
                    pipeInfoTileDataLs = _pipeInfoData.Submittals;
                    break;
            }

            List<string> tilesThatNeeded = new List<string>();

            for (int i=0;i<pipeInfoTileDataLs.Count;i++)
            {
                InfoDescriptionTileWidgetController ctrl = _infoDescTileWidCtrlLs.Find(x => x.Props.Id == i.ToString());

                if (ctrl != null)
                {
                    ctrl.Refresh(new InfoDescriptionTileWidgetProperties(true, ctrl.Props.Id, pipeInfoTileDataLs[i].Heading, pipeInfoTileDataLs[i].DescriptionInfoLs));
                }
                else
                {
                    _infoDescTileWidCtrlLs.Add(CreateTile(new InfoDescriptionTileWidgetProperties(true, i.ToString(), pipeInfoTileDataLs[i].Heading, pipeInfoTileDataLs[i].DescriptionInfoLs)));
                }

                tilesThatNeeded.Add(i.ToString());
            }

            for (int i = 0; i < _infoDescTileWidCtrlLs.Count; i++)
            {
                bool thisTileNeeded = tilesThatNeeded.Exists(x => x == _infoDescTileWidCtrlLs[i].Props.Id);
                if (thisTileNeeded == false)
                {
                    DeleteTile(i);
                    i = 0;
                }
            }
            tilesThatNeeded.Clear();
        }

        private InfoDescriptionTileWidgetController CreateTile(InfoDescriptionTileWidgetProperties props)
        {
            GameObject tileGO = Instantiate(_infoTilePrfb, new Vector3(0, 0, 0), Quaternion.identity);
            tileGO.transform.SetParent(_infoTileParentT, false);

            InfoDescriptionTileWidgetController ctrl = tileGO.GetComponent<InfoDescriptionTileWidgetController>();
            ctrl.LinkClicked_EventHandler += OnClickLink;
            ctrl.SetData(props);

            return ctrl;
        }

        private void DeleteTile(int index)
        {
            if (index >= 0)
            {
                Destroy(_infoDescTileWidCtrlLs[index].gameObject);
                _infoDescTileWidCtrlLs.RemoveAt(index);
            }
        }
    }
}
