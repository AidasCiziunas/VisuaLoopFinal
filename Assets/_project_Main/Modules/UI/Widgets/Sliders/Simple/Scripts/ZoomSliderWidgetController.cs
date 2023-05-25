using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class ZoomSliderWidgetController : MonoBehaviour
    {
        public event EventHandler<ZoomSliderChangeEventArgs> SliderValChanged_EventHandler;

        public float SliderVal { get => _slider.value; }
        public ZoomSliderWidgetProperties Props { get => _props; }
        public string SliderValStr { get => _sliderValStr; }

        [SerializeField] Slider _slider;
        [SerializeField] TextMeshProUGUI _upperValTxt;
        [SerializeField] TextMeshProUGUI _lowerValTxt;

        private ZoomSliderWidgetProperties _props;
        private string _sliderValStr = "0";

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChangedSlider);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.AddListener(OnValueChangedSlider);
        }

        public void SetData(ZoomSliderWidgetProperties props)
        {
            _props = props;

            _slider.value = (_props.InitialVal - _props.MinVal) / (_props.MaxVal - _props.MinVal);

            _upperValTxt.text = _props.MaxVal.ToString() + "%";
            _lowerValTxt.text = _props.MinVal.ToString() + "%";
        }

        public void Refresh(ZoomSliderWidgetProperties props)
        {
            SetData(props);
        }

        private void OnValueChangedSlider(float val)
        {
            _sliderValStr = (val * (_props.MaxVal - _props.MinVal) + _props.MinVal).ToString("F0");
            SliderValChanged_EventHandler?.Invoke(this, new ZoomSliderChangeEventArgs(SliderVal));
        }
    }
}
