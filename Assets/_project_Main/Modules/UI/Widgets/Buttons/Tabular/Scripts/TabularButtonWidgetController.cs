using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class TabularButtonWidgetController : MonoBehaviour
    {
        public TabularButtonWidgetProperties Props { get => _props; }

        public event EventHandler MainBtnPointerClicked_EventHandler;

        [SerializeField] UIButton _mainBtn;
        [SerializeField] TextMeshProUGUI _mainTxt;
        [SerializeField] GameObject _highlighterGo;

        private TabularButtonWidgetProperties _props;

        private void OnEnable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickMainBtn);
        }

        private void OnDisable()
        {
            _mainBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickMainBtn);
        }

        public void SetData(TabularButtonWidgetProperties props)
        {
            _props = props;

            SetupWidget();
        }

        public void Refresh(TabularButtonWidgetProperties props)
        {
            SetData(props);
        }

        private void OnPointerClickMainBtn()
        {
            MainBtnPointerClicked_EventHandler?.Invoke(this, new EventArgs());
        }

        private void SetupWidget()
        {
            _mainTxt.text = _props.BtnText;

            switch(_props.State)
            {
                case TabularBtnState.Normal:
                    _highlighterGo.SetActive(false);

                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 0.68f);
                    break;
                case TabularBtnState.Selected:
                    _highlighterGo.SetActive(true);

                    _mainTxt.color = new Color(_mainTxt.color.r, _mainTxt.color.g, _mainTxt.color.b, 1.0f);
                    break;
            }
        }
    }
}
