using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class InstagramButtonWidgetController : MonoBehaviour
    {
        public event EventHandler<InstagramBtnClickedEventArgs> MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;

        private InstagramButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(InstagramButtonWidgetProperties props)
        {
            _props = props;
        }

        public void Refresh(InstagramButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new InstagramBtnClickedEventArgs(_props.URL));
        }
    }
}
