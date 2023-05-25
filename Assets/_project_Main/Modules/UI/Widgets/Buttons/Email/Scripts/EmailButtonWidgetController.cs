using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class EmailButtonWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;
        public EmailButtonWidgetProperties Props { get => _props; }

        [SerializeField] UIButton _mainBtn;
        [SerializeField] TextMeshProUGUI _mainTxt;

        private EmailButtonWidgetProperties _props;


        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(EmailButtonWidgetProperties props)
        {
            _props = props;

            _mainTxt.text = _props.MainText;

            _mainBtn.interactable = _props.Interactable;
        }

        public void Refresh(EmailButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }
    }
}
