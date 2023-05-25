using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class BgCanvasController : MonoBehaviour
    {
        [SerializeField] Vector2Animator _fullModeAnim;
        [SerializeField] Vector2Animator _smallModeAnim;

        private void OnEnable()
        {
            UIModuleSignals.Get<MainMenuSimpleWindowSignal.DrawerInteraction>().AddListener(OnDrawerInteraction);
        }

        private void OnDisable()
        {
            UIModuleSignals.Get<MainMenuSimpleWindowSignal.DrawerInteraction>().RemoveListener(OnDrawerInteraction);
        }

        private Void OnDrawerInteraction(DrawerBehaviourType drawerBehaviourType, float heightDiff)
        {
            switch(drawerBehaviourType)
            {
                case DrawerBehaviourType.Opened:
                    _fullModeAnim.Play();
                    break;
                case DrawerBehaviourType.Closed:
                    _smallModeAnim.Play();
                    break;
            }

            return null;
        }
    }
}
