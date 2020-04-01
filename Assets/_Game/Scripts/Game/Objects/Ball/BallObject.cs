using Assets._Game.Scripts.Game.Handlers.Direction;
using Assets._Game.Scripts.Game.Handlers.Speed;
using Assets._Game.Scripts.Game.Objects.Moving.Allower;
using UnityEngine;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Ball
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(MoveAllower))]
    class BallObject : AttachingMonoBehaviour, IBall
    {
        #region PRIVATE_VALUES
        private Rigidbody _rigidbody;
        private ISpeedHandler _speedHandler { get; set; }
        private IDirectionHandler _directionsHandler { get; set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool moving { get; private set; }
        public bool canMove { get; private set; }
        #endregion // PUBLIC_VALUES

        //////////////////////////////////////////////

        #region MONO_BEHAVIOUR
        private void FixedUpdate()
        {
            if (canMove && moving)
                UpdateVelocityState(Time.fixedDeltaTime);
        }
        #endregion // MONO_BEHAVIOUR

        #region ATTACHING
        protected override void OnAttaching()
            => Attach(ref _rigidbody);
        #endregion // ATTACHING

        #region INJECT
        [Inject]
        public void Construct(ISpeedHandler speedHandler, IDirectionHandler directionHandler)
        {
            _directionsHandler = directionHandler;
            _speedHandler = speedHandler;
        }
        #endregion // INJECT

        #region PUBLIC_METHODS
        public void StartMove()
            => SetMoving(true);

        public void StopMove()
        {
            SetMoving(false);
            _speedHandler.Reset();
            UpdateVelocityState(0);
        }

        public void ChangeDirection()
        {
            if (moving == false)
                return;

            _directionsHandler.MoveNext();
        }

        public void SetPermissionToMove(bool status)
        {
            if (canMove == status)
                return;

            canMove = status;
        }

        public void SetPositionOnSurface(Vector3 positionOnSurface)
        {
            var halfBallSize = transform.localScale / 2f;
            transform.position = positionOnSurface + Vector3.Scale(Vector3.up, halfBallSize);
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void SetMoving(bool status)
        {
            if (status == moving)
                return;

            moving = status;
        }

        private void UpdateVelocityState(float deltaTime)
        {
            if (_rigidbody == null)
                return;

            _rigidbody.velocity = _directionsHandler.current * _speedHandler.speed;
            _speedHandler.IncreaseSpeed(deltaTime);
        }
        #endregion // PRIVATE_METHODS
    }
}
