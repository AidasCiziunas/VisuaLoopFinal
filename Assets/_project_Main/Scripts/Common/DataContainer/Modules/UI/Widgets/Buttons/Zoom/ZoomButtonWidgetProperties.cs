using System;

namespace SyedAli.Main
{
    public enum ZoomButtonState
    {
        Normal,
        Pressed,
        Disabled
    }

    [Serializable]
    public class ZoomButtonWidgetProperties
    {
        public readonly ZoomButtonState InteractionToolButtonState;
        public readonly string MainText;
        public readonly bool Visibility;

        public ZoomButtonWidgetProperties(ZoomButtonState interactionToolButtonState, string mainText, bool visibility)
        {
            InteractionToolButtonState = interactionToolButtonState;
            MainText = mainText;
            Visibility = visibility;
        }
    }
}
