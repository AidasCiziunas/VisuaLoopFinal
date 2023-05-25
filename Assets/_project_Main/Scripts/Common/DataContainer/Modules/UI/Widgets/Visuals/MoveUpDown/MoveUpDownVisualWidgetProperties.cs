using System;
using UnityEngine;

namespace SyedAli.Main
{
    [Serializable]
    public class MoveUpDownVisualWidgetProperties
    {
        public readonly string MainText;

        public MoveUpDownVisualWidgetProperties(string mainText)
        {
            MainText = mainText;
        }
    }
}
