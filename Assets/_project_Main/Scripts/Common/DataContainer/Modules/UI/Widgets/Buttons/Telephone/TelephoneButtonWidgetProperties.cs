using System;

namespace SyedAli.Main
{
    [Serializable]
    public class TelephoneButtonWidgetProperties
    {
        public readonly bool Interactable;
        public readonly string MainText;

        public TelephoneButtonWidgetProperties(bool interactable, string mainText)
        {
            Interactable = interactable;
            MainText = mainText;
        }
    }
}
