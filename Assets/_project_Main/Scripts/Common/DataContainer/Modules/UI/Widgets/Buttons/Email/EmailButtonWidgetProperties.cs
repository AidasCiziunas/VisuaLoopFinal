using System;

namespace SyedAli.Main
{
    [Serializable]
    public class EmailButtonWidgetProperties
    {
        public readonly bool Interactable;
        public readonly string MainText;

        public EmailButtonWidgetProperties(bool interactable, string mainText)
        {
            Interactable = interactable;
            MainText = mainText;
        }
    }
}
