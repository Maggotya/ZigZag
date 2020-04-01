using UnityEngine;

namespace Assets._Game.Scripts.Game.Input
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "ScriptableObjects/InputConfig")]
    class InputConfig : ScriptableObject, IInputConfig
    {
        #region SERIALIZE_FIELDS
        [Tooltip("Позволяет поворачивать тапом по экрану")]
        [SerializeField] private bool _ChangeByTouch;

        [Tooltip("Позволяет поворачивать кликом мыши")]
        [SerializeField] private bool _ChangeByClick;

        [Tooltip("Позволяет поворачивать клавишей на клавиатуре. None - отключено.")]
        [SerializeField] private KeyCode _ChangeDirectionKey;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public bool changeByTouch => _ChangeByTouch;
        public bool changeByClick => _ChangeByClick;
        public KeyCode changeDirectionKey => _ChangeDirectionKey;
        #endregion // PUBLIC_VALUES
    }
}
