using System.Collections.Generic;
using System.Linq;
using Assets._Game.Scripts.Game.Objects.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Moving.Catcher
{
    [RequireComponent(typeof(Collider))]
    class MovableCatcher : MonoBehaviour, IMovableCatcher
    {
        #region SERIALIZE_FIELDS
        [Header("Events")]
        [SerializeField] private UnityEvent _OnBecomeBusy;
        [SerializeField] private UnityEvent _OnBecomeFree;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_VALUES
        private SafeHashSet<IMovable> _movablesOnPlatform = new SafeHashSet<IMovable>();
        private bool _hasMovables => _movablesOnPlatform.Any();
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool inContact { get; private set; }

        public UnityEvent onBecomeBusy {
            get => _OnBecomeBusy;
            set => _OnBecomeBusy = value;
        }

        public UnityEvent onBecomeFree {
            get => _OnBecomeFree;
            set => _OnBecomeFree = value;
        }
        #endregion // PUBLIC_VALUES

        ///////////////////////////////////////////////

        #region MONO_BEHAVIOUR
        private void OnTriggerEnter(Collider collider)
        {
            if (TryGetMovable(collider, out var movable)) {
                _movablesOnPlatform.Add(movable);
                UpdateContactedStatus();
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (TryGetMovable(collider, out var movable)) {
                _movablesOnPlatform.Remove(movable);
                UpdateContactedStatus();
            }
        }
        #endregion // MONO_BEHAVIOUR

        #region RESET
        public void Reset()
        {
            _movablesOnPlatform.Clear();
            SetInContact(false);
        }
        #endregion // RESET

        #region MOVABLE_HANDLING
        private bool TryGetMovable(Collider collider, out IMovable movable)
            => ( movable = GetMovable(collider) ) != null;

        private IMovable GetMovable(Collider collider)
            => collider?.gameObject?.GetComponent<IMovable>();
        #endregion // MOVABLE_HANDLING

        #region ACTIVE_STATUS_HANDLING
        private void UpdateContactedStatus()
        {
            if (inContact == false && _hasMovables)
                SetBusy();

            if (inContact && _hasMovables == false)
                SetFree();
        }

        private void SetBusy()
        {
            SetInContact(true);
            onBecomeBusy?.Invoke();
        }

        private void SetFree()
        {
            SetInContact(false);
            onBecomeFree?.Invoke();
        }

        private void SetInContact(bool status)
        {
            if (inContact == status)
                return;

            inContact = status;
        }
        #endregion // ACTIVE_STATUS_HANDLING
    }
}
