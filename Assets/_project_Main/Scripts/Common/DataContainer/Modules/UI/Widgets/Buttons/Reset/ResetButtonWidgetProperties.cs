using System;

namespace SyedAli.Main
{
    public enum ResetButtonState
    {
        Normal,
        Pressed
    }

    [Serializable]
    public class ResetButtonWidgetProperties
    {
        public readonly ResetButtonState InteractionToolButtonState;
        public readonly string MainText;
        public readonly bool Interactable;

        public ResetButtonWidgetProperties(ResetButtonState interactionToolButtonState, string mainText, bool interactable)
        {
            InteractionToolButtonState = interactionToolButtonState;
            MainText = mainText;
            Interactable = interactable;
        }
    }
}
