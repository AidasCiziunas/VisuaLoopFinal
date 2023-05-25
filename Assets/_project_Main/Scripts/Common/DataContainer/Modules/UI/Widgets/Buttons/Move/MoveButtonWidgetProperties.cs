using System;

namespace SyedAli.Main
{
    public enum MoveButtonState
    {
        Normal,
        Pressed,
        Disabled
    }

    [Serializable]
    public class MoveButtonWidgetProperties
    {
        public readonly MoveButtonState InteractionToolButtonState;
        public readonly string MainText;
        public readonly bool Visibility;

        public MoveButtonWidgetProperties(MoveButtonState interactionToolButtonState, string mainText, bool visibility)
        {
            InteractionToolButtonState = interactionToolButtonState;
            MainText = mainText;
            Visibility = visibility;
        }
    }
}
