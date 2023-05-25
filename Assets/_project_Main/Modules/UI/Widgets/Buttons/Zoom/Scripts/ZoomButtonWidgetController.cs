using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class ZoomButtonWidgetController : MonoBehaviour
    {
        public event EventHandler MainBtnPointerClicked_EventHandler;
        public ZoomButtonWidgetProperties Props { get => _props; }

        [SerializeField] GameObject _thisGo;
        [Space(10)]

        [SerializeField] UIButton _mainBtn;

        [Space(10)]
        [SerializeField] GameObject _shadowGo;
        [SerializeField] GameObject _pressedGo;
        [SerializeField] GameObject _strokeGo;

        [Space(10)]
        [SerializeField] Image _iconImg;

        [Space(10)]
        [SerializeField] TextMeshProUGUI _mainTxt;

        private ZoomButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(ZoomButtonWidgetProperties props)
        {
            _props = props;

            SetupButton();

            _thisGo.SetActive(_props.Visibility);
        }

        public void Refresh(ZoomButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }

        private void SetupButton()
        {
            _mainTxt.text = _props.MainText;

            switch (_props.InteractionToolButtonState)
            {
                case ZoomButtonState.Normal:
                    _shadowGo.SetActive(true);
                    _pressedGo.SetActive(false);
                    _strokeGo.SetActive(false);
                    _iconImg.color = new Color(_iconImg.color.r, _iconImg.color.g, _iconImg.color.b, 0.68f);

                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 0.68f);
                    _mainBtn.enabled = true;
                    break;
                case ZoomButtonState.Pressed:
                    _shadowGo.SetActive(false);
                    _pressedGo.SetActive(true);
                    _strokeGo.SetActive(true);
                    _iconImg.color = new Color(_iconImg.color.r, _iconImg.color.g, _iconImg.color.b, 1f);

                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 1f);
                    _mainBtn.enabled = true;
                    break;
                case ZoomButtonState.Disabled:
                    _shadowGo.SetActive(false);
                    _pressedGo.SetActive(false);
                    _strokeGo.SetActive(false);
                    _iconImg.color = new Color(_iconImg.color.r, _iconImg.color.g, _iconImg.color.b, 1f);

                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 1f);
                    _mainBtn.enabled = false;
                    break;
            }
        }
    }
}
