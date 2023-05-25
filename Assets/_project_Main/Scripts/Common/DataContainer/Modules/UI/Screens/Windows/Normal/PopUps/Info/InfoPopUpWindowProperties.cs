using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace SyedAli.Main
{
    public enum TabularBtnType
    {
        Detail, Brochures, BIMContent, Submittals
    }

    [Serializable]
    public class TabularBtnData
    {
        public TabularBtnType Type;
        public TabularButtonWidgetController Ctrl;
    }

    [Serializable]
    public class InfoPopUpWindowProperties : WindowProperties
    {
        public readonly UnityAction OnPopUpCloseAct;

        public InfoPopUpWindowProperties(UnityAction onPopUpCloseAct)
        {
            OnPopUpCloseAct = onPopUpCloseAct;
        }
    }

    [Serializable]
    public class PipeInfoData
    {
        public List<PipeInfoTileData> Details;
        public List<PipeInfoTileData> DocBrochers;
        public List<PipeInfoTileData> BIMContent;
        public List<PipeInfoTileData> Submittals;
    }

    [Serializable]
    public class PipeInfoTileData
    {
        public string Heading;
        public List<DescriptionInfo> DescriptionInfoLs;
    }

    [Serializable]
    public class DescriptionInfo
    {
        public string Description;
        public string URL;
    }
}


