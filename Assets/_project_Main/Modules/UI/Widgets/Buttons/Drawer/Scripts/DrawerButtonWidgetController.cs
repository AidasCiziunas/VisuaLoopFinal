using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class DrawerButtonWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;
        public DrawerButtonWidgetProperties Props { get => _props; }

        [SerializeField] UIButton _mainBtn;
        [SerializeField] Transform _iconT;

        private DrawerButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(DrawerButtonWidgetProperties props)
        {
            _props = props;

            SetupWidget();
        }

        public void Refresh(DrawerButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }

        private void SetupWidget()
        {
            switch(_props.DrawerBtnState)
            {
                case DrawerBtnState.Up:
                    _iconT.localEulerAngles = new Vector3(_iconT.localEulerAngles.x, _iconT.localEulerAngles.y, 0);
                    break;
                case DrawerBtnState.Down:
                    _iconT.localEulerAngles = new Vector3(_iconT.localEulerAngles.x, _iconT.localEulerAngles.y, 180);
                    break;
            }
        }
    }
}
