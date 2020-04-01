using Assets._Game.Scripts.Instruments.Counter;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Assets._Game.Scripts.Game.Scoring
{
    class Score : MonoBehaviour, IScore
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private IntUnityEvent _OnChanged;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_VALUES
        private ICounter<int> _counter { get; set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VLUES
        public int score => _counter?.current ?? 0;
        public IntUnityEvent onChanged {
            get => _OnChanged;
            set => _OnChanged = value;
        }
        #endregion // PUBLIC_VLUES

        ///////////////////////////////////////////////////////////

        #region INJECTS
        [Inject]
        public void Construct(IScoreParameters parameters)
        {
            _counter = new IntCounter(0, parameters.scorePerGem, int.MaxValue, false);
            _counter.onChanged += (value) => onChanged?.Invoke(value);
            onChanged?.Invoke(_counter.current);
        }
        #endregion // INJECTS

        #region PUBLIC_METHODS
        public void AddScore()
            => _counter?.Increase();

        public void SubtractScore()
            => _counter?.Dicrease();
        #endregion // PUBLIC_METHODS
    }
}
