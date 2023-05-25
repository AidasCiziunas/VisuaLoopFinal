using SyedAli.Main.UIModule.Widgets;
using System;
using System.Threading;
using UnityEngine;

namespace SyedAli.Main
{
    public enum PipeTileBtnState
    {
        Normal,
        Pressed
    }

    [Serializable]
    public class PipeTileWidgetProperties
    {
        public readonly PipeType Id;
        public readonly PipeTileBtnState PipeTileButtonState;
        public readonly Sprite Image;
        public readonly string Name;

        public PipeTileWidgetProperties(PipeType id, PipeTileBtnState pipeTileButtonState, Sprite image, string name)
        {
            Id = id;
            PipeTileButtonState = pipeTileButtonState;
            Image = image;
            Name = name;
        }
    }

    public class PipeTileBtnEventArgs : EventArgs
    {
        public readonly PipeTileWidgetProperties Props;

        public PipeTileBtnEventArgs(PipeTileWidgetProperties props)
        {
            Props = props;
        }
    }
}
