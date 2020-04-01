using System;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Classes
{
    [CreateAssetMenu(fileName = "GameParameters", menuName = "ScriptableObjects/GameParameters")]
    public class GameParameters : ScriptableObject, IGameParameters
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private BallParameters _Ball;
        [SerializeField] private PlatformParameters _Platform;
        [SerializeField] private DirectionParameters _Direction;
        [SerializeField] private DifficultyParameters _Difficulty;
        [SerializeField] private PlatformGeneratorParameters _PlatformGenerator;
        [SerializeField] private GemGeneratorParameters _GemGenerator;
        [SerializeField] private ScoreParameters _Score;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public IBallParameters Ball => _Ball;
        public IPlatformParameters Platform => _Platform;
        public IDirectionParameters Direction => _Direction;
        public IDifficultyParameters Difficulty => _Difficulty;
        public IPlatformGeneratorParameters PlatformGenerator => _PlatformGenerator;
        public IGemGeneratorParameters GemGenerator => _GemGenerator;
        public IScoreParameters Score => _Score;
        #endregion // PUBLIC_VALUES
    }
}