using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class WebsiteButtonWidgetController : MonoBehaviour
    {
        public event EventHandler<WebsiteBtnClickedEventArgs> MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;

        private WebsiteButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(WebsiteButtonWidgetProperties props)
        {
            _props = props;
        }

        public void Refresh(WebsiteButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new WebsiteBtnClickedEventArgs(_props.URL));
        }
    }
}
