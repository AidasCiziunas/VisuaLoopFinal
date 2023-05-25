using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class SimpleButtonWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;
        [SerializeField] TextMeshProUGUI _mainTxt;

        private SimpleButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(SimpleButtonWidgetProperties props)
        {
            _props = props;

            _mainTxt.text = _props.MainText;

            _mainBtn.interactable = _props.Interactable;
        }

        public void Refresh(SimpleButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }
    }
}
