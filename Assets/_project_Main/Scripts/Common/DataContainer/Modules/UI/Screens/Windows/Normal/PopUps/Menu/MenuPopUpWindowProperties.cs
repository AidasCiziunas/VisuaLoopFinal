using System;
using UnityEngine;
using UnityEngine.Events;

namespace SyedAli.Main
{
    [Serializable]
    public class MenuPopUpWindowProperties : WindowProperties
    {
        public readonly UnityAction CloseAct;
        public readonly UnityAction<Transform> TopBarAct;

        public MenuPopUpWindowProperties(UnityAction closeAct, UnityAction<Transform> topBarAct = null)
        {
            CloseAct = closeAct;
            TopBarAct = topBarAct;
        }
    }
}


