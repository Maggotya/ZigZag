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

        #region PRIVATE_VALUES
        private bool _canHandleTouch => _Config.changeByTouch && Input.touchSupported;
        private bool _canHandleClick => _Config.changeByClick && !_canHandleTouch;
        #endregion // PRIVATE_VALUES

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
            if (_canHandleClick == false)
                return;

            if (EventSystem.current?.IsPointerOverGameObject() == true || EventSystem.current?.currentSelectedGameObject != null) 
                return;

            if (Input.GetMouseButtonUp(0))
                _OnGameKeyPress?.Invoke();
        }

        private void HandleTouchPress()
        {
            if (_canHandleTouch == false)
                return;

            foreach (var touch in Input.touches) {
                if (EventSystem.current?.IsPointerOverGameObject(touch.fingerId) == true || EventSystem.current?.currentSelectedGameObject != null)
                    return;

                if (touch.phase.Equals(TouchPhase.Ended)) {
                    _OnGameKeyPress?.Invoke();
                    return;
                }
            }
        }
        #endregion // PRIVATE_METHODS

    }
}
