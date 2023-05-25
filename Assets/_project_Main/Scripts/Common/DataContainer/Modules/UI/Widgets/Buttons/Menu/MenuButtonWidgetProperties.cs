using System;

namespace SyedAli.Main
{
    [Serializable]
    public class MenuButtonWidgetProperties
    {
        public readonly bool Interactable;

        public MenuButtonWidgetProperties(bool interactable)
        {
            Interactable = interactable;
        }
    }
}
