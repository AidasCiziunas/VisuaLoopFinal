using System;
using UnityEngine;

namespace SyedAli.Main
{
    [Serializable]
    public class LoadingVisualWidgetProperties
    {
        public readonly bool RotationStatus;

        public LoadingVisualWidgetProperties(bool rotationStatus)
        {
            RotationStatus = rotationStatus;
        }
    }
}
