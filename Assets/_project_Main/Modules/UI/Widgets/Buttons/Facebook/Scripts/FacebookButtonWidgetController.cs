using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class FacebookButtonWidgetController : MonoBehaviour
    {
        public event EventHandler<FacebookBtnClickedEventArgs> MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;

        private FacebookButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(FacebookButtonWidgetProperties props)
        {
            _props = props;
        }

        public void Refresh(FacebookButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new FacebookBtnClickedEventArgs(_props.URL));
        }
    }
}
