using System;
using UnityEngine;

namespace SyedAli.Main
{
    [Serializable]
    public class MoveLeftRightVisualWidgetProperties
    {
        public readonly string MainText;

        public MoveLeftRightVisualWidgetProperties(string mainText)
        {
            MainText = mainText;
        }
    }
}
