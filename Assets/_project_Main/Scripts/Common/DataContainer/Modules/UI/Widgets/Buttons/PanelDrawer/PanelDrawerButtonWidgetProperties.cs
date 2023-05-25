using System;

namespace SyedAli.Main
{
    [Serializable]
    public class PanelDrawerButtonWidgetProperties
    {
        public readonly bool EnableStatus;

        public PanelDrawerButtonWidgetProperties(bool enableStatus)
        {
            EnableStatus = enableStatus;
        }
    }
}
