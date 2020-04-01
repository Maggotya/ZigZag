using Assets._Game.Scripts.Instruments.Counter;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Gem.Variator
{
    class RandomVariator : IVariator
    {
        #region PRIVATE_VALUES
        private ICounter<int> _counter { get; set; }
        private int _targetIndex { get; set; }
        #endregion // PRIVATE_VALUES

        ////////////////////////////////////////

        #region CONSTRUCTORS
        public RandomVariator(int platformsInBlock)
            => _counter = new IntCounter(1, 1, 1, platformsInBlock, true);
        #endregion // CONSTRUCTORS

        ////////////////////////////////////////

        #region PUBLIC_METHODS
        public bool IsSuccess()
        {
            CheckCycleStarting();

            var result = _counter.current == _targetIndex;
            _counter.Increase();

            return result;
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void CheckCycleStarting()
        {
            if (_counter.current == 1)
                _targetIndex = Random.Range(1, _counter.end + 1);
        }
        #endregion // PRIVATE_METHODS
    }
}
