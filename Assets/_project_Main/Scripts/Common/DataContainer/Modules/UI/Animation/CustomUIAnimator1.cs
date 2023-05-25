using Doozy.Runtime.Reactor.Animators;
using UnityEngine;

namespace SyedAli.Main
{
    public class CustomUIAnimator1 : MonoBehaviour
    {
        [SerializeField] UIAnimator _uIAnim;

        public UIAnimator UIAnim { get => _uIAnim; set => _uIAnim = value; }
    }
}
