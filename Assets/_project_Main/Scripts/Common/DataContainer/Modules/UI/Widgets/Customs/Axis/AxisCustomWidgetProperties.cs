using System;

namespace SyedAli.Main
{
    public enum AxisSide
    {
        None,
        Pos,
        Neg
    }

    [Serializable]
    public class AxisCustomWidgetProperties
    {
        public readonly bool ResetAxis;
        public readonly string XText;
        public readonly string YText;
        public readonly string ZText;

        public AxisCustomWidgetProperties(bool resetAxis, string xText, string yText, string zText)
        {
            ResetAxis = resetAxis;
            XText = xText;
            YText = yText;
            ZText = zText;
        }
    }

    public class AxisBtnEventArgs : EventArgs
    {
        public readonly AxisSide AxisSide;

        public AxisBtnEventArgs(AxisSide axisSide)
        {
            this.AxisSide = axisSide;
        }
    }

}
