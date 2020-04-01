using System;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Classes
{
    [Serializable]
    class ScoreParameters : IScoreParameters
    {
        [Min(0)] [Tooltip("Количество очков за каждый пойманный кристалл")]
        [SerializeField] private int _ScorePerGem;

        public int scorePerGem => _ScorePerGem;
    }
}
