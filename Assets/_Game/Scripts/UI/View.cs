using System;
using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    class View : AttachingMonoBehaviour, IView
    {
        #region PRIVATE_VALUES
        private CanvasGroup _canvasGroup;
        private Coroutine _animation { get; set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool active { get; private set; }
        #endregion // PUBLIC_VALUES

        #region CONSTS
        private const float ENABLE_ALPHA = 1;
        private const float DISABLE_ALPHA = 0;
        private const float ANIMATION_TIME = 0.3f;
        #endregion // CONSTS

        //////////////////////////

        #region MONO_BEHAVIOUR
        private void Start()
            => active = gameObject.activeInHierarchy;

        private void OnDisable()
            => StopAnimation();
        #endregion // MONO_BEHAVIOUR

        #region ATTACHING
        protected override void OnAttaching()
            => Attach(ref _canvasGroup);
        #endregion // ATTACHING

        #region PUBLIC_METHODS
        public void Enable()
        {
            if (active)
                return;
            
            SetActive(true);
            SetEnabled(true);
            StartChangeAlpha(ENABLE_ALPHA, ANIMATION_TIME);
        }

        public void Disable()
        {
            if (active == false)
                return;

            SetActive(false);
            StartChangeAlpha(DISABLE_ALPHA, ANIMATION_TIME,
                () => SetEnabled(false));
        }

        public void SetActive(bool status)
        {
            if (active == status)
                return;

            active = status;
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void SetEnabled(bool status)
        {
            if (gameObject.activeSelf == status)
                return;

            gameObject.SetActive(status);
        }

        private void StopAnimation()
        {
            if (_animation != null) {
                StopCoroutine(_animation);
                _animation = null;
            }
        }

        private Coroutine StartChangeAlpha(float targetAlpha, float animationTime, Action callback = null)
        {
            StopAnimation();
            if (gameObject.activeInHierarchy)
                _animation = StartCoroutine(ChangeAlpha(targetAlpha, animationTime, callback));

            return _animation;
        }
        private IEnumerator ChangeAlpha(float targetAlpha, float animationTime, Action callback = null)
        {
            if (_canvasGroup == null)
                yield break;

            var startAlpha = _canvasGroup.alpha;

            for (var t = 0f; t < animationTime; t += Time.unscaledDeltaTime) {
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / animationTime);
                yield return null;
            }

            _canvasGroup.alpha = targetAlpha;
            callback?.Invoke();
        }
        #endregion // PRIVATE_METHODS
    }
}
