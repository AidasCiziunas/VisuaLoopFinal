using System;

namespace SyedAli.Main
{
    public enum InfoButtonState
    {
        Normal,
        Pressed,
        Disabled
    }

    [Serializable]
    public class InfoButtonWidgetProperties
    {
        public readonly InfoButtonState InteractionToolButtonState;
        public readonly string MainText;
        public readonly bool Visibility;

        public InfoButtonWidgetProperties(InfoButtonState interactionToolButtonState, string mainText, bool visibility)
        {
            InteractionToolButtonState = interactionToolButtonState;
            MainText = mainText;
            Visibility = visibility;
        }
    }
}
