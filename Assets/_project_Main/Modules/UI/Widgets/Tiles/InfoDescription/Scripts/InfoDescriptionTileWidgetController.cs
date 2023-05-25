using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class InfoDescriptionTileWidgetController : MonoBehaviour
    {
        public event EventHandler LinkClicked_EventHandler;

        public InfoDescriptionTileWidgetProperties Props { get => _props; }
        
        [SerializeField] TextMeshProUGUI _headingTxt;

        [Space(10)]
        [SerializeField] GameObject _descTextTilePrfb;
        [SerializeField] Transform _descTextTileParentT;

        private InfoDescriptionTileWidgetProperties _props;
        private List<DescTextTileWidgetController> _descTextTileWidCtrlLs = new List<DescTextTileWidgetController>();

        private void OnDisable()
        {
            foreach(var entry in _descTextTileWidCtrlLs)
            {
                entry.MainBtnPointerClicked_EventHandler -= OnClickDescTextLink;
            }
        }

        public void SetData(InfoDescriptionTileWidgetProperties props)
        {
            _props = props;

            SetupWidget();
        }

        public void Refresh(InfoDescriptionTileWidgetProperties props)
        {
            SetData(props);
        }

        public void DirectRefresh(InfoDescriptionTileWidgetProperties props)
        {
            for (int i = 0; i < _props.DescriptionInfoLs.Count; i++)
            {
                DescTextTileWidgetController ctrl = _descTextTileWidCtrlLs.Find(x => x.Props.Id == i.ToString());
                ctrl.Refresh(new DescTextTileWidgetProperties(props.Interactable, ctrl.Props.Id, _props.DescriptionInfoLs[i].Description, _props.DescriptionInfoLs[i].URL));
            }
        }

        private void SetupWidget()
        {
            _headingTxt.text = _props.HeadingText;
            SetupTile();
        }

        private void SetupTile()
        {
            List<string> tilesThatNeeded = new List<string>();

            for (int i = 0; i < _props.DescriptionInfoLs.Count; i++)
            {
                DescTextTileWidgetController ctrl = _descTextTileWidCtrlLs.Find(x => x.Props.Id == i.ToString());

                if (ctrl != null)
                {
                    ctrl.Refresh(new DescTextTileWidgetProperties(true, ctrl.Props.Id, _props.DescriptionInfoLs[i].Description, _props.DescriptionInfoLs[i].URL));
                }
                else
                {
                    _descTextTileWidCtrlLs.Add(CreateTile(new DescTextTileWidgetProperties(true, i.ToString(), _props.DescriptionInfoLs[i].Description, _props.DescriptionInfoLs[i].URL)));
                }

                tilesThatNeeded.Add(i.ToString());
            }

            for (int i = 0; i < _descTextTileWidCtrlLs.Count; i++)
            {
                bool thisTileNeeded = tilesThatNeeded.Exists(x => x == _descTextTileWidCtrlLs[i].Props.Id);
                if (thisTileNeeded == false)
                {
                    DeleteTile(i);
                    i = 0;
                }
            }
            tilesThatNeeded.Clear();
        }

        private DescTextTileWidgetController CreateTile(DescTextTileWidgetProperties props)
        {
            GameObject tileGO = Instantiate(_descTextTilePrfb, new Vector3(0, 0, 0), Quaternion.identity);
            tileGO.transform.SetParent(_descTextTileParentT, false);

            DescTextTileWidgetController ctrl = tileGO.GetComponent<DescTextTileWidgetController>();
            ctrl.MainBtnPointerClicked_EventHandler += OnClickDescTextLink;
            ctrl.SetData(props);

            return ctrl;
        }

        private void DeleteTile(int index)
        {
            if (index >= 0)
            {
                Destroy(_descTextTileWidCtrlLs[index].gameObject);
                _descTextTileWidCtrlLs.RemoveAt(index);
            }
        }

        private void OnClickDescTextLink(object sender, EventArgs e)
        {
            LinkClicked_EventHandler?.Invoke(this, new EventArgs());
        }
    }
}