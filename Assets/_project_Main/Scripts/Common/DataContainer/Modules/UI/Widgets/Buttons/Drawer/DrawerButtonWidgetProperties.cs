using System;

namespace SyedAli.Main
{
    public enum DrawerBtnState
    {
        Up, Down
    }

    [Serializable]
    public class DrawerButtonWidgetProperties
    {
        public readonly DrawerBtnState DrawerBtnState;

        public DrawerButtonWidgetProperties(DrawerBtnState drawerBtnState)
        {
            DrawerBtnState = drawerBtnState;
        }
    }
}
