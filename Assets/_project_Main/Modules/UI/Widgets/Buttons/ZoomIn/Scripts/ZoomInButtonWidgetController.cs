using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class ZoomInButtonWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;

        [SerializeField] GameObject _thisGo;
        [Space(10)]

        [SerializeField] UIButton _mainBtn;

        private ZoomInButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(ZoomInButtonWidgetProperties props)
        {
            _props = props;

            _mainBtn.interactable = _props.Interactable;
        }

        public void Refresh(ZoomInButtonWidgetProperties props)
        {
            SetData(props);
        }

        public void Visibility(bool status)
        {
            _thisGo.SetActive(status);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }
    }
}
