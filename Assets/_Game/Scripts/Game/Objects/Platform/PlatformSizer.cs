using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Platform
{
    [RequireComponent(typeof(Transform))]
    class PlatformSizer : MonoBehaviour, IPlatformSizer
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private float _Size;
        [SerializeField] private float _Thickness;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public float size {
            get => _Size;
            set {
                _Size = value;
                UpdateSize();
            }
        }

        public float thickness {
            get => _Thickness;
            set {
                _Thickness = value;
                UpdateSize();
            }
        }
        #endregion // PUBLIC_VALUES

        ////////////////////////////////////

        #region MONO_BEHAVIOUR
        private void OnValidate()
            => UpdateSize();
        #endregion // MONO_BEHAVIOUR

        #region PUBLIC_METHODS
        public void SetSize(float size)
            => this.size = size;
        public void SetThickness(float thickness)
            => this.thickness = thickness;
        public void Set(float size, float thickness)
        {
            SetSize(size);
            SetThickness(thickness);
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void UpdateSize()
        {
            if (transform == null)
                return;

            transform.localScale = new Vector3(size, thickness, size);
        }
        #endregion // PRIVATE_METHODS
    }
}
