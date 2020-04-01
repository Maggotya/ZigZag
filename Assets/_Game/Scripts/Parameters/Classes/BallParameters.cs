using System;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Classes
{
    [Serializable]
    class BallParameters : IBallParameters
    {
        #region SERIALIZE_FIELDS
        [Tooltip("Начальная/постоянная скорость")]
        [SerializeField] private float _StartSpeed;

        [Tooltip("Ускорение. 0 - равномерное движение")]
        [SerializeField] private float _Acceleration;

        [Tooltip("Нижняя граница скорости")]
        [SerializeField] private float _MinSpeed;

        [Tooltip("Верхняя граница скорости")]
        [SerializeField] private float _MaxSpeed;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public float startSpeed => _StartSpeed;
        public float acceleration => _Acceleration;
        public float minSpeed => _MinSpeed;
        public float maxSpeed => _MaxSpeed;
        #endregion // PUBLIC_VALUES
    }
}
