using System;

namespace SyedAli.Main
{
    public enum TabularBtnState
    {
        Normal,
        Selected
    }

    [Serializable]
    public class TabularButtonWidgetProperties
    {
        public readonly TabularBtnState State;
        public readonly string BtnText;

        public TabularButtonWidgetProperties(TabularBtnState state, string btnText)
        {
            State = state;
            BtnText = btnText;
        }
    }
}
