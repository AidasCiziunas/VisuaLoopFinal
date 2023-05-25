using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class TelephoneButtonWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;
        [SerializeField] TextMeshProUGUI _mainTxt;

        private TelephoneButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(TelephoneButtonWidgetProperties props)
        {
            _props = props;

            _mainTxt.text = _props.MainText;

            _mainBtn.interactable = _props.Interactable;
        }

        public void Refresh(TelephoneButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }
    }
}
