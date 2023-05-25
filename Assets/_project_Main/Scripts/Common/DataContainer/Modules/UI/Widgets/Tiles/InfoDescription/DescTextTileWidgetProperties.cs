using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SyedAli.Main
{
    [Serializable]
    public class DescTextTileWidgetProperties
    {
        public readonly bool Interactable;
        public readonly string Id;
        public readonly string DescText;
        public readonly string URL;

        public DescTextTileWidgetProperties(bool interactable, string id, string descText, string uRL)
        {
            Interactable = interactable;
            Id = id;
            DescText = descText;
            URL = uRL;
        }
    }
}
