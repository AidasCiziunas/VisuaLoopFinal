using Doozy.Runtime.Reactor;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class RotateVisualWidgetController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTxt;

        private RotateVisualWidgetProperties _props;

        public void SetData(RotateVisualWidgetProperties props)
        {
            _props = props;
            _mainTxt.text = _props.MainText;
        }

        public void Refresh(RotateVisualWidgetProperties props)
        {
            SetData(props);
        }
    }
}
