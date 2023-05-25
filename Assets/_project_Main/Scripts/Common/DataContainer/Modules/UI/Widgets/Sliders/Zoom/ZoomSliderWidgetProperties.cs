using System;

namespace SyedAli.Main
{
    [Serializable]
    public class ZoomSliderWidgetProperties
    {
        public readonly float InitialVal;
        public readonly float MinVal;
        public readonly float MaxVal;

        public ZoomSliderWidgetProperties(float initialVal, float minVal, float maxVal)
        {
            InitialVal = initialVal;
            MinVal = minVal;
            MaxVal = maxVal;
        }
    }

    public class ZoomSliderChangeEventArgs : EventArgs
    {
        public readonly float SliderVal;

        public ZoomSliderChangeEventArgs(float sliderVal)
        {
            SliderVal = sliderVal;
        }
    }
}
