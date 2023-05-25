using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class LinkedinButtonWidgetController : MonoBehaviour
    {
        public event EventHandler<LinkedinBtnClickedEventArgs> MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;

        private LinkedinButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(LinkedinButtonWidgetProperties props)
        {
            _props = props;
        }

        public void Refresh(LinkedinButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new LinkedinBtnClickedEventArgs(_props.URL));
        }
    }
}
