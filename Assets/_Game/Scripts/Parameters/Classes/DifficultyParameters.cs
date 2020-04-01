using System;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Classes
{
    public enum Difficulty
    { Easy, Medium, Hard }

    [Serializable]
    class DifficultyParameters : IDifficultyParameters
    {
        #region SERIALZIE_FIELDS
        [Tooltip("Сложность")]
        [SerializeField] private Difficulty _Current;

        [Min(1)] [Tooltip("Ширина пути для шарика в лёгком режиме. Ширина - в платформах")]
        [SerializeField] private int _StepWidth_Easy;

        [Min(1)] [Tooltip("Ширина пути для шарика в среднем режиме. Ширина - в платформах")]
        [SerializeField] private int _StepWidth_Medium;

        [Min(1)] [Tooltip("Ширина пути для шарика в сложном режиме. Ширина - в платформах")]
        [SerializeField] private int _StepWidth_Hard;
        #endregion // SERIALZIE_FIELDS

        #region PUBLIC_VALUES
        public Difficulty current => _Current;
        public int stepWidth => StepWidth(current);

        public int StepWidth(Difficulty difficulty)
        {
            switch(difficulty) {
                case Difficulty.Easy: return _StepWidth_Easy;
                case Difficulty.Medium: return _StepWidth_Medium;
                case Difficulty.Hard: return _StepWidth_Hard;
                default: return 0;
            }
        }
        #endregion // PUBLIC_VALUES
    }
}
