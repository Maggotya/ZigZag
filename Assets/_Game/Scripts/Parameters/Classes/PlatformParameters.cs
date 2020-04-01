using System;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Classes
{
    [Serializable]
    class PlatformParameters : IPlatformParameters
    {
        #region SERIALIZE_FIELDS
        [Tooltip("Размер платформы в горизонтальной плоскости")]
        [SerializeField] private float _Size;

        [Tooltip("Размер платформы в вертикальной плоскости. Толщина")]
        [SerializeField] private float _Thickness;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public float size => _Size;
        public float thickness => _Thickness;
        #endregion // PUBLIC_VALUES
    }
}
