using System;
using System.Linq;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Classes
{
    [Serializable]
    class DirectionParameters : IDirectionParameters
    {
        #region SERIZALZIE_FIELDS
        [Tooltip("Доступные направления движения. Меняются поочерёдно. Ограничений в количестве нет. По умолчанию Forward и Right")]
        [SerializeField] private DirectionTypes[] _Directions;
        #endregion // SERIZALZIE_FIELDS

        #region PUBLIC_VALUES
        public Vector3[] directions => _Directions.Select(d => DirectionTypeToVector(d)).ToArray();
        #endregion // PUBLIC_VALUES

        #region PRIVATE_STRUCTS
        private enum DirectionTypes
        { Forward, Back, Left, Right }
        #endregion // PRIVATE_STRUCTS

        #region PRIVATE_METHODS
        private Vector3 DirectionTypeToVector(DirectionTypes type)
        {
            switch (type) {
                case DirectionTypes.Forward: return Vector3.forward;
                case DirectionTypes.Back: return Vector3.back;
                case DirectionTypes.Right: return Vector3.right;
                case DirectionTypes.Left: return Vector3.left;
                default: return Vector3.zero;
            }
        }
        #endregion // PRIVATE_METHODS
    }
}
