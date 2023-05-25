using System;

namespace SyedAli.Main
{
    public enum ThreeDButtonState
    {
        Normal,
        Pressed,
        Disabled
    }

    [Serializable]
    public class ThreeDButtonWidgetProperties
    {
        public readonly ThreeDButtonState InteractionToolButtonState;
        public readonly string MainText;
        public readonly bool Visibility;

        public ThreeDButtonWidgetProperties(ThreeDButtonState interactionToolButtonState, string mainText, bool visibility)
        {
            InteractionToolButtonState = interactionToolButtonState;
            MainText = mainText;
            Visibility = visibility;
        }
    }
}
