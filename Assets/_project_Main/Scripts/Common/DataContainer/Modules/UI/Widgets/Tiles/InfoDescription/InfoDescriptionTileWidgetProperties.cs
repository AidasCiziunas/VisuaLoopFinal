using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SyedAli.Main
{
    [Serializable]
    public class InfoDescriptionTileWidgetProperties
    {
        public readonly bool Interactable;
        public readonly string Id;
        public readonly string HeadingText;
        public readonly List<DescriptionInfo> DescriptionInfoLs;

        public InfoDescriptionTileWidgetProperties(bool interactable, string id, string headingText, List<DescriptionInfo> descriptionInfoLs)
        {
            Interactable = interactable;
            Id = id;
            HeadingText = headingText;
            DescriptionInfoLs = descriptionInfoLs;
        }
    }

    public class InfoDescriptionTileBtnEventArgs : EventArgs
    {
        public readonly InfoDescriptionTileWidgetProperties Props;

        public InfoDescriptionTileBtnEventArgs(InfoDescriptionTileWidgetProperties props)
        {
            Props = props;
        }
    }
}
