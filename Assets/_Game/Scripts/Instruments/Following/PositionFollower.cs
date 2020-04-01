using UnityEngine;

namespace Assets._Game.Scripts.Instruments.Following
{
    class PositionFollower : MonoBehaviour, IPositionFollower
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private Transform _FollowedObject;
        [SerializeField] private bool _UseOffset;
        [SerializeField] private Vector3 _Offset;
        [SerializeField] private Vector3 _MovingMask = Vector3.one;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_VALUES
        private Vector3 _lastPosition { get; set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public Transform followedObject => _FollowedObject;
        public Vector3 movingMask => _MovingMask;
        public bool useOffset => _UseOffset;
        public Vector3 offset => _Offset;
        #endregion // PUBLIC_VALUES

        //////////////////////////////////////////

        #region SERIALIZE_VALIDATION
        private bool _lastUseOffset { get; set; }

        private void OnValidate()
        {
            if (_FollowedObject == null)
                return;

            if (_lastUseOffset != _UseOffset) {
                _lastUseOffset = _UseOffset;

                if (_lastUseOffset)
                    _Offset = transform.position - _FollowedObject.position;
            }

            if (_UseOffset)
                UpdatePosition();
        }
        #endregion // SERIALIZE_VALIDATION

        #region MONO_BEHAVIOUR
        private void Start()
        {
            CorrectMask(ref _MovingMask);
            SetFollowedObject(_FollowedObject);
            UpdatePosition();
        }

        private void Update()
        {
            if (_FollowedObject && _FollowedObject.position.Equals(_lastPosition) == false)
                UpdatePosition();
        }
        #endregion // MONO_BEHAVIOUR

        /////////////////////////////////////////

        #region PUBLIC_METHODS
        public void SetFollowedObject(Transform transform)
        {
            _FollowedObject = transform;
            _lastPosition = transform ? transform.position : Vector3.zero;
        }

        public void SetOffset(Vector3 offset, bool status)
        {
            SetOffset(status);
            SetOffset(offset);
        }

        public void SetOffset(Vector3 offset)
        {
            _Offset = offset;
            if (_UseOffset)
                UpdatePosition();
        }

        public void SetOffset(bool status)
        {
            if (_UseOffset == status)
                return;

            _UseOffset = status;
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void CorrectMask(ref Vector3 mask)
        {
            for(var i = 0; i < 3; i++)
                mask[i] = mask[i].Equals(0) ? 0 : 1;
        }

        private void UpdatePosition()
        {
            transform.position += _maskedDirectionToMove;
            _lastPosition = _FollowedObject.position;
        }

        private Vector3 _maskedDirectionToMove
            => Vector3.Scale(_MovingMask, _unmaskedDirectionToMove);
        private Vector3 _unmaskedDirectionToMove
            => _UseOffset ?
            _FollowedObject.position + _Offset - transform.position :
            _FollowedObject.position - _lastPosition;
        #endregion // PRIVATE_METHODS
    }
}
