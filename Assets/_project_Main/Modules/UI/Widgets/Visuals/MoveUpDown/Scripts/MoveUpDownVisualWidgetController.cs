using Doozy.Runtime.Reactor;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class MoveUpDownVisualWidgetController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTxt;

        private MoveUpDownVisualWidgetProperties _props;

        public void SetData(MoveUpDownVisualWidgetProperties props)
        {
            _props = props;

            if (_mainTxt != null)
                _mainTxt.text = _props.MainText;
        }

        public void Refresh(MoveUpDownVisualWidgetProperties props)
        {
            SetData(props);
        }
    }
}
