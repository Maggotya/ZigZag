using Assets._Game.Scripts.Instruments.Counter;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Handlers.Direction
{
    class DirectionHandler : IDirectionHandler
    {
        #region PUBLIC_VALUES
        public Vector3 current => GetDirection(_counter.current);
        public Vector3 next => GetDirection(_counter.next);
        public Vector3 previous => GetDirection(_counter.prev);
        #endregion // PUBLIC_VALUES

        #region PRIVATE_VALUES
        private bool _noDirections { get; set; }
        private Vector3[] directions { get; set; }
        private ICounter<int> _counter { get; set; }
        #endregion // PRIVATE_VALUES

        ///////////////////////////////////////////

        #region CONSTRUCTORS
        public DirectionHandler() : this(new Vector3[0])
        { }

        public DirectionHandler(params Vector3[] directions)
        {
            this.directions = directions;
            _counter = GetCounter(directions);
            _noDirections = directions.Length == 0;
        }
        #endregion // CONSTRUCTORS

        #region PUBLIC_METHODS
        public Vector3 MoveNext()
        {
            _counter.Increase();
            return current;
        }

        public Vector3 MovePrevious()
        {
            _counter.Dicrease();
            return current;
        }

        public void Reset()
            => _counter.Reset();
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private ICounter<int> GetCounter(Vector3[] directions)
            => new IntCounter(
                current: 0,
                step: 1,
                start: 0,
                end: directions.Length - 1,
                looped: true);

        private Vector3 GetDirection(int index)
            => _noDirections ?
                Vector3.zero :
                directions[index];
        #endregion // PRIVATE_METHODS
    }
}
