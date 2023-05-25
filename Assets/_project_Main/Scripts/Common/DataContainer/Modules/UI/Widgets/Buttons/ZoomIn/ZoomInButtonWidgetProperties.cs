using System;

namespace SyedAli.Main
{
    [Serializable]
    public class ZoomInButtonWidgetProperties
    {
        public readonly bool Interactable;

        public ZoomInButtonWidgetProperties(bool interactable)
        {
            Interactable = interactable;
        }
    }
}
