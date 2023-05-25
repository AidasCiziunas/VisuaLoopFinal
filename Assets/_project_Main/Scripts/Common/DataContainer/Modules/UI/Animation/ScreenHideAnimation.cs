using Doozy.Runtime.Reactor.Animators;
using System;
using UnityEngine;

namespace SyedAli.Main
{
    public class ScreenHideAnimation : ATransitionComponent
    {
        [SerializeField] UIAnimator _uIAnimator;
        [SerializeField] bool _onHideMoveScreenOutOfBound = true; //When it is false, then we can test the standalone screen without worrying of already going out of bounds in test canvas


        private Action _callWhenFinished;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = this.GetComponent<CanvasGroup>();
        }

        public override bool IsAnimPlaying()
        {
            return _uIAnimator.animation.isPlaying;
        }

        public override void Animate(Transform target, Action callWhenFinished)
        {
            _callWhenFinished = callWhenFinished;
            _uIAnimator.animation.OnPlayCallback.AddListener(OnPlayAnim);
            _uIAnimator.animation.OnStopCallback.AddListener(OnStopAnim);
            _uIAnimator.animation.OnFinishCallback.AddListener(OnFinishAnim);

            _uIAnimator.Play();
        }

        private void OnPlayAnim()
        {
            _canvasGroup.blocksRaycasts = false;
        }

        private void OnStopAnim()
        {
            _canvasGroup.blocksRaycasts = true;
            _callWhenFinished?.Invoke();

            _uIAnimator.animation.OnPlayCallback.RemoveListener(OnPlayAnim);
            _uIAnimator.animation.OnStopCallback.RemoveListener(OnStopAnim);
            _uIAnimator.animation.OnFinishCallback.RemoveListener(OnFinishAnim);

            if (_onHideMoveScreenOutOfBound)
            {
                this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height);
            }
        }

        private void OnFinishAnim()
        {
            _canvasGroup.blocksRaycasts = true;
            _callWhenFinished?.Invoke();

            _uIAnimator.animation.OnPlayCallback.RemoveListener(OnPlayAnim);
            _uIAnimator.animation.OnStopCallback.RemoveListener(OnStopAnim);
            _uIAnimator.animation.OnFinishCallback.RemoveListener(OnFinishAnim);

            if (_onHideMoveScreenOutOfBound)
            {
                this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height);
            }
        }
    }
}
