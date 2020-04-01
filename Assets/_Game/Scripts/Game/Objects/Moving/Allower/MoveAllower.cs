using System.Collections.Generic;
using System.Linq;
using Assets._Game.Scripts.Game.Objects.Moving.Surface;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Moving.Allower
{
    [RequireComponent(typeof(Collider))]
    public class MoveAllower : MonoBehaviour, IMoveAllower
    {
        #region SERIALIZE_FIELDS
        [Header("State")]
        [SerializeField] private bool _Allowed;

        [Header("Events")]
        [SerializeField] private BoolUnityEvent _OnStatusChanged;
        [SerializeField] private UnityEvent _OnSurfaceEnter;
        [SerializeField] private UnityEvent _OnSurfaceExit;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_VALUES
        private SafeHashSet<ISurfaceToMove> _movableSurfaces = new SafeHashSet<ISurfaceToMove>();
        private bool _hasMovableSurfaces => _movableSurfaces.Any();
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool allowed => _Allowed;

        public BoolUnityEvent onStatusChanged {
            get => _OnStatusChanged;
            set => _OnStatusChanged = value;
        }

        public UnityEvent onSurfaceEnter {
            get => _OnSurfaceEnter;
            set => _OnSurfaceEnter = value;
        }

        public UnityEvent onSurfaceExit {
            get => _OnSurfaceExit;
            set => _OnSurfaceExit = value;
        }
        #endregion // PUBLIC_VALUES

        ///////////////////////////////////////////////

        #region MONO_BEHAVIOUR
        private void OnCollisionEnter(Collision collision)
        {
            if (IsHandledCollision(collision, out var surface)) {
                _movableSurfaces.Add(surface);
                onSurfaceEnter?.Invoke();
                UpdateAllowance();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (IsHandledCollision(collision, out var surface)) {
                _movableSurfaces.Remove(surface);
                onSurfaceExit?.Invoke();
                UpdateAllowance();
            }
        }
        #endregion // MONO_BEHAVIOUR

        #region SURFACE_HANDLING
        private bool IsHandledCollision(Collision collision, out ISurfaceToMove surface)
            => TryGetSurface(collision, out surface) && IsSurfaceAllowed(surface);

        private bool TryGetSurface(Collision collision, out ISurfaceToMove surface)
            => ( surface = GetSurface(collision) ) != null;

        private ISurfaceToMove GetSurface(Collision collision)
            => collision?.gameObject?.GetComponent<ISurfaceToMove>();

        private bool IsSurfaceAllowed(ISurfaceToMove surface)
            => !surface.Equals(null) && surface.allowMove;
        #endregion // SURFACE_HANDLING

        #region RESET
        public void Reset()
        {
            _movableSurfaces.Clear();
            UpdateAllowance();
        }
        #endregion // RESET

        #region ALLOWANCE
        private void UpdateAllowance()
            => SetAllowed(_hasMovableSurfaces);

        private void SetAllowed(bool status)
        {
            if (status == _Allowed)
                return;

            _Allowed = status;
            onStatusChanged?.Invoke(_Allowed);
        }
        #endregion // ALLOWANCE
    }
}
