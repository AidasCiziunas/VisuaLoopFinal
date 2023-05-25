using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class ResetButtonWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;
        public ResetButtonWidgetProperties Props { get => _props; }

        [SerializeField] UIButton _mainBtn;

        [Space(10)]
        [SerializeField] GameObject _shadowGo;
        [SerializeField] GameObject _pressedGo;
        [SerializeField] GameObject _strokeGo;

        [Space(10)]
        [SerializeField] Image _iconImg;

        [Space(10)]
        [SerializeField] TextMeshProUGUI _mainTxt;

        private ResetButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
            _mainBtn.pressedState.stateEvent.Event.AddListener(OnPressedStateMainBtn);
            _mainBtn.normalState.stateEvent.Event.AddListener(OnNormalStateMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
            _mainBtn.pressedState.stateEvent.Event.RemoveListener(OnPressedStateMainBtn);
            _mainBtn.normalState.stateEvent.Event.RemoveListener(OnNormalStateMainBtn);
        }

        public void SetData(ResetButtonWidgetProperties props)
        {
            _props = props;

            SetupButton();

            _mainBtn.interactable = _props.Interactable;
        }

        public void Refresh(ResetButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }

        private void OnPressedStateMainBtn()
        {
            ResetButtonWidgetProperties props = new ResetButtonWidgetProperties(ResetButtonState.Pressed, _props.MainText, true);
            _props = props;
            SetupButton();
        }

        private void OnNormalStateMainBtn()
        {
            ResetButtonWidgetProperties props = new ResetButtonWidgetProperties(ResetButtonState.Normal, _props.MainText, true);
            _props = props;
            SetupButton();
        }

        private void SetupButton()
        {
            _mainTxt.text = _props.MainText;

            switch (_props.InteractionToolButtonState)
            {
                case ResetButtonState.Normal:
                    _shadowGo.SetActive(true);
                    _pressedGo.SetActive(false);
                    _strokeGo.SetActive(false);
                    _iconImg.color = new Color(_iconImg.color.r, _iconImg.color.g, _iconImg.color.b, 0.68f);

                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 0.68f);
                    break;
                case ResetButtonState.Pressed:
                    _shadowGo.SetActive(false);
                    _pressedGo.SetActive(true);
                    _strokeGo.SetActive(true);
                    _iconImg.color = new Color(_iconImg.color.r, _iconImg.color.g, _iconImg.color.b, 1.0f);

                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 1.0f);
                    break;
            }
        }
    }
}
