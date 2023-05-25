using System;

namespace SyedAli.Main
{
    [Serializable]
    public class SimpleButtonWidgetProperties
    {
        public readonly bool Interactable;
        public readonly string MainText;

        public SimpleButtonWidgetProperties(bool interactable, string mainText)
        {
            Interactable = interactable;
            MainText = mainText;
        }
    }
}
