using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class MenuButtonWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;

        private MenuButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(MenuButtonWidgetProperties props)
        {
            _props = props;

            _mainBtn.interactable = _props.Interactable;
        }

        public void Refresh(MenuButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }
    }
}
