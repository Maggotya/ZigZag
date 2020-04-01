using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.FallArea
{
    [RequireComponent(typeof(Collider))]
    class FallAreaObject : MonoBehaviour, IFallArea
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private GameObject _ObjectMustFall;
        [SerializeField] private UnityEvent _OnObjectFallen;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public GameObject objectMustFall => _ObjectMustFall;
        public UnityEvent onObjectFallen {
            get => _OnObjectFallen;
            set => _OnObjectFallen = value;
        }
        #endregion // PUBLIC_VALUES

        ///////////////////////////////////////////////////

        #region MONO_BEHAVIOUR
        private void OnTriggerEnter(Collider other)
        {
            if (objectMustFall && other?.gameObject && other.gameObject.Equals(objectMustFall))
                onObjectFallen?.Invoke();
        }
        #endregion // MONO_BEHAVIOUR
    }
}
