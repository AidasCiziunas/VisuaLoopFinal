using System;
using UnityEngine;

namespace SyedAli.Main
{
    [Serializable]
    public class RotateVisualWidgetProperties
    {
        public readonly string MainText;

        public RotateVisualWidgetProperties(string mainText)
        {
            MainText = mainText;
        }
    }
}
