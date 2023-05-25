using Doozy.Runtime.Reactor;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class AxisCustomWidgetController : MonoBehaviour
    {
        public event EventHandler<AxisBtnEventArgs> YBtnPointerClicked_EventHandler;
        public event EventHandler<AxisBtnEventArgs> XBtnPointerClicked_EventHandler;
        public event EventHandler<AxisBtnEventArgs> ZBtnPointerClicked_EventHandler;

        public AxisCustomWidgetProperties Props { get => _props; }

        [Space(10)]
        [SerializeField] UIButton _yBtn;
        [SerializeField] TextMeshProUGUI _yBtnTxt;

        [Space(10)]
        [SerializeField] UIButton _zBtn;
        [SerializeField] TextMeshProUGUI _zBtnTxt;

        [Space(10)]
        [SerializeField] UIButton _xBtn;
        [SerializeField] TextMeshProUGUI _xBtnTxt;

        private AxisCustomWidgetProperties _props;

        private AxisSide _currentYAxisSide;
        private AxisSide _currentZAxisSide;
        private AxisSide _currentXAxisSide;

        private void OnEnable()
        {
            _yBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickYBtn);
            _zBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickZBtn);
            _xBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(OnPointerClickXBtn);
        }

        private void OnDisable()
        {
            _yBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickYBtn);
            _zBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickZBtn);
            _xBtn.behaviours.GetBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(OnPointerClickXBtn);
        }

        public void SetData(AxisCustomWidgetProperties props)
        {
            _props = props;

            SetupWidget();
        }

        public void Refresh(AxisCustomWidgetProperties props)
        {
            SetData(props);
        }

        public void Visibility(bool status)
        {
            this.gameObject.SetActive(status);
        }

        private void OnPointerClickYBtn()
        {
            AxisSide axisSideToShow = AxisSide.None;

            if (_currentYAxisSide == AxisSide.None)
                axisSideToShow = AxisSide.Pos;
            else if (_currentYAxisSide == AxisSide.Pos)
                axisSideToShow = AxisSide.Neg;
            else if (_currentYAxisSide == AxisSide.Neg)
                axisSideToShow= AxisSide.Pos;

            YBtnPointerClicked_EventHandler?.Invoke(this, new AxisBtnEventArgs(axisSideToShow));

            _currentYAxisSide = axisSideToShow;
            _currentZAxisSide = AxisSide.None;
            _currentXAxisSide = AxisSide.None;
        }

        private void OnPointerClickZBtn()
        {
            AxisSide axisSideToShow = AxisSide.None;

            if (_currentZAxisSide == AxisSide.None)
                axisSideToShow = AxisSide.Pos;
            else if (_currentZAxisSide == AxisSide.Pos)
                axisSideToShow = AxisSide.Neg;
            else if (_currentZAxisSide == AxisSide.Neg)
                axisSideToShow = AxisSide.Pos;

            ZBtnPointerClicked_EventHandler?.Invoke(this, new AxisBtnEventArgs(axisSideToShow));

            _currentYAxisSide = AxisSide.None;
            _currentZAxisSide = axisSideToShow;
            _currentXAxisSide = AxisSide.None;
        }

        private void OnPointerClickXBtn()
        {
            AxisSide axisSideToShow = AxisSide.None;

            if (_currentXAxisSide == AxisSide.None)
                axisSideToShow = AxisSide.Pos;
            else if (_currentXAxisSide == AxisSide.Pos)
                axisSideToShow = AxisSide.Neg;
            else if (_currentXAxisSide == AxisSide.Neg)
                axisSideToShow = AxisSide.Pos;

            XBtnPointerClicked_EventHandler?.Invoke(this, new AxisBtnEventArgs(axisSideToShow));

            _currentYAxisSide = AxisSide.None;
            _currentZAxisSide = AxisSide.None;
            _currentXAxisSide = axisSideToShow;
        }

        private void SetupWidget()
        {
            if (_props.ResetAxis)
            {
                _currentYAxisSide = AxisSide.None;
                _currentZAxisSide = AxisSide.None;
                _currentXAxisSide = AxisSide.None;
            }

            _yBtnTxt.text = _props.YText;
            _zBtnTxt.text = _props.ZText;
            _xBtnTxt.text = _props.XText;
        }
    }
}
