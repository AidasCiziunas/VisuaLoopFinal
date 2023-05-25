using System;

namespace SyedAli.Main
{
    public enum InteractionType
    {
        None, ThreeD, Move, Zoom, Info
    }

    public enum InteractionPanelState
    {
        Open, Close
    }

    [Serializable]
    public class InteractionToolsPanelProperties : PanelProperties
    {
    }
}


