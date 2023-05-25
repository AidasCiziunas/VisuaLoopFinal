using Doozy.Runtime.Reactor;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SyedAli.Main.UIModule.Widgets
{
    public class MoveLeftRightVisualWidgetController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTxt;

        private MoveLeftRightVisualWidgetProperties _props;

        public void SetData(MoveLeftRightVisualWidgetProperties props)
        {
            _props = props;

            if (_mainTxt != null)
                _mainTxt.text = _props.MainText;
        }

        public void Refresh(MoveLeftRightVisualWidgetProperties props)
        {
            SetData(props);
        }
    }
}
