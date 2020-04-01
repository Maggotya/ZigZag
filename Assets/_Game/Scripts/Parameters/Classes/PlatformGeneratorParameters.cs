using System;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;


namespace Assets._Game.Scripts.Parameters.Classes
{
    [Serializable]
    class PlatformGeneratorParameters : IPlatformGeneratorParameters
    {
        #region SERIALIZE_FIELDS
        [Range(0, 100)] [Tooltip("Вероятность, с которой направление движение дороги меняет своё направление")]
        [SerializeField] private float _ProbailityChangeDirection;

        [Min(0)] [Tooltip("Количество платформ, без учёта ширины дороги, которое может оставаться после шарика")]
        [SerializeField] private int _StepThresholdBack;

        [Min(0)] [Tooltip("Количество платформ, без учёта ширины дороги, которое видит игрок")]
        [SerializeField] private int _StepsThresholdForward;

        [Min(0)] [Tooltip("Количество платформ, без учёта ширины дороги, которое будет установленно фиксированно " +
            "в первом указанном в Directoins направлении. Чтобы на старте уровня не было резкого ухода в сторону.")]
        [SerializeField] private int _InitialStepsOneDirection;

        [Min(1)] [Tooltip("Размер квадрата из платформ, на котором появляется шарик.")]
        [SerializeField] private int _InitialAreaSize;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public float probabilityChangeDirection => _ProbailityChangeDirection;
        public int stepThresholdBack => _StepThresholdBack;
        public int stepThresholdForward => _StepsThresholdForward;
        public int initialStepsOneDirection => _InitialStepsOneDirection;
        public int initialAreaSize => _InitialAreaSize;
        #endregion // PUBLIC_VALUES
    }
}
