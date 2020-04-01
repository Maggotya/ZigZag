using Assets._Game.Scripts.Instruments.Counter;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Gem.Variator
{
    class IterativeVariator : IVariator
    {
        #region PRIVATE_VALUES
        private ICounter<int> _counter { get; set; }
        private ICounter<int> _targetIndex { get; set; }
        #endregion // PRIVATE_VALUES

        /////////////////////////////

        #region CONSTRUCTORS
        public IterativeVariator(int platformsInBlock, int iterateOffset)
        {
            _counter = new IntCounter(1, 1, 1, platformsInBlock, true);
            _targetIndex = new IntCounter(1, iterateOffset, 1, platformsInBlock, true);
        }
        #endregion // CONSTRUCTORS

        /////////////////////////////

        #region PUBLIC_METHODS
        public bool IsSuccess()
        {
            var result = _counter.current == _targetIndex.current;
            CheckCycleEnding();
            _counter.Increase();

            return result;
        }

        public void Reset()
        {
            _counter.Reset();
            _targetIndex.Reset();
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void CheckCycleEnding()
        {
            if (_counter.current == _counter.end)
                _targetIndex.Increase();
        }
        #endregion // PRIVATE_METHODS
    }
}
