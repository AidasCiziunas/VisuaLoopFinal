using System;

namespace SyedAli.Main
{
    [Serializable]
    public class ZoomOutButtonWidgetProperties
    {
        public readonly bool Interactable;

        public ZoomOutButtonWidgetProperties(bool interactable)
        {
            Interactable = interactable;
        }
    }
}
