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
    public class DescTextTileWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;

        public DescTextTileWidgetProperties Props { get => _props; }
        
        [SerializeField] UIButton _mainBtn;
        [SerializeField] TextMeshProUGUI _descTxt;

        private DescTextTileWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(DescTextTileWidgetProperties props)
        {
            _props = props;
            SetupWidget();
        }

        public void Refresh(DescTextTileWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            //MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
            Application.OpenURL(_props.URL);
        }

        private void SetupWidget()
        {
            _descTxt.text = _props.DescText;

            if (_props.URL != null)
            {
                _descTxt.fontStyle = FontStyles.Underline;
                _mainBtn.interactable = _props.Interactable;
            }
            else
            {
                _descTxt.fontStyle = FontStyles.Normal;
                _mainBtn.interactable = false;
            }
        }
    }
}