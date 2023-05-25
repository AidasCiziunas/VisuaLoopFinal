using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class PipeTileWidgetController : MonoBehaviour
    {
        public event EventHandler<PipeTileBtnEventArgs> MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;

        [Space(10)]
        [SerializeField] GameObject _shadowGo;
        [SerializeField] GameObject _pressedGo;
        [SerializeField] GameObject _strokeGo;

        [Space(10)]
        [SerializeField] Image _pipeImg;

        [Space(10)]
        [SerializeField] TextMeshProUGUI _mainTxt;

        private PipeTileWidgetProperties _props;

        public PipeTileWidgetProperties Props { get => _props; }

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(PipeTileWidgetProperties props)
        {
            _props = props;

            SetupWidget();
        }

        public void Refresh(PipeTileWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new PipeTileBtnEventArgs(_props));
        }

        private void SetupWidget()
        {
            _pipeImg.sprite = _props.Image;
            _mainTxt.text = _props.Name;

            switch (_props.PipeTileButtonState)
            {
                case PipeTileBtnState.Normal:
                    _shadowGo.SetActive(true);
                    _pressedGo.SetActive(false);
                    _strokeGo.SetActive(false);
                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 0.68f);
                    break;
                case PipeTileBtnState.Pressed:
                    _shadowGo.SetActive(false);
                    _pressedGo.SetActive(true);
                    _strokeGo.SetActive(false);
                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 1f);
                    break;
            }
        }
    }
}