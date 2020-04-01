using Assets._Game.Scripts.Game.Handlers.Direction;
using Assets._Game.Scripts.Game.Handlers.Probability;
using UnityEngine;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Changer
{
    class DirectionChangerByProbability : IDirectionChanger
    {
        #region PRIVATE_VALUES
        private IDirectionHandler _directionHandler { get; set; }
        private IProbabilitier _probabilitier { get; set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public Vector3 currentDirection => _directionHandler.current;
        #endregion // PUBLIC_VALUES

        ///////////////////////////////////////////

        #region CONSTRUCTORS
        [Inject]
        public DirectionChangerByProbability(IDirectionHandler directionHandler, IProbabilitier probabilitier)
        {
            _directionHandler = directionHandler;
            _probabilitier = probabilitier;
        }
        #endregion // CONSTRUCTORS

        //////////////////////////////////////////

        #region PUBLIC_METHODS
        public Vector3 ChangeDirection()
        {
            if (_probabilitier.IsSuccess())
                _directionHandler.MoveNext();

            return _directionHandler.current;
        }

        public void Reset()
        {
            _directionHandler.Reset();
            _probabilitier.Reset();
        }
        #endregion // PUBLIC_METHODS
    }
}
