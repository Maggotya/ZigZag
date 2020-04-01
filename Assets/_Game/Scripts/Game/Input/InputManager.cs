using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets._Game.Scripts.Game.Input
{
    using Input = UnityEngine.Input;

    class InputManager : MonoBehaviour, IInputManager
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private UnityEvent _OnGameKeyPress;
        #endregion // SERIALIZE_FIELDS

        #region INJECTS
        [Inject] private IInputConfig _Config;
        #endregion // INJECTS

        #region PUBLIC_VALUES
        public UnityEvent onGameKeyPress {
            get => _OnGameKeyPress;
            set => _OnGameKeyPress = value;
        }
        #endregion // PUBLIC_VALUES

        //////////////////////////////////////

        #region MONO_BEHAVIOUR
        private void Update()
        {
            if (_Config == null)
                return;

            HandleKeyPress();
            HandleMousePress();
            HandleTouchPress();
        }
        #endregion // MONO_BEHAVIOUR

        #region PRIVATE_METHODS
        private void HandleKeyPress()
        {
            if (Input.GetKeyUp(_Config.changeDirectionKey))
                _OnGameKeyPress?.Invoke();
        }

        private void HandleMousePress()
        {
            if (EventSystem.current?.IsPointerOverGameObject() == true)
                return;

            if (_Config.changeByClick && Input.GetMouseButtonUp(0))
                _OnGameKeyPress?.Invoke();
        }

        private void HandleTouchPress()
        {
            var currentTouch = 0;

            if (EventSystem.current?.IsPointerOverGameObject(currentTouch) == true)
                return;

            if (_Config.changeByTouch && Input.touchCount > 0 && Input.touches[currentTouch].phase.Equals(TouchPhase.Ended))
                _OnGameKeyPress?.Invoke();
        }
        #endregion // PRIVATE_METHODS

    }
}
