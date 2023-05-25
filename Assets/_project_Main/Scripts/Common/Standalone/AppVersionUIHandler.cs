using TMPro;
using UnityEngine;

namespace SyedAli.Main
{
    public class AppVersionUIHandler : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _apkVersionTxt;

        private void Start()
        {
            _apkVersionTxt.text = "Version: " + Application.version;
        }
    }
}
