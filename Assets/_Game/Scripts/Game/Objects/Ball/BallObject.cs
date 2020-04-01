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
        private void Start()
            => CacheValues();

        private void FixedUpdate()
        {
            if (canMove)
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

        public void Reset()
        {
            _speedHandler.Reset();
            _directionsHandler.Reset();

            ResetFromCache();
        }
        #endregion // PUBLIC_METHODS

        #region CACHING
        private Vector3 _initialPositionCache { get; set; }
        private Vector3 _initialVelocityCache { get; set; }
        private bool _canMoveCache { get; set; }
        private bool _movingCache { get; set; }

        private void CacheValues()
        {
            _initialPositionCache = transform.position;
            _initialVelocityCache = _rigidbody?.velocity ?? Vector3.zero;

            _canMoveCache = canMove;
            _movingCache = moving;
        }

        private void ResetFromCache()
        {
            canMove = _canMoveCache;
            moving = _movingCache;

            transform.position = _initialPositionCache;
            if (_rigidbody) _rigidbody.velocity = _initialVelocityCache;
        }
        #endregion // CACHING

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

            // в случае, когда шарик не движется, принудительно задаём ему нулевую скорость,
            // чтобы погасить остаточные силы.
            if (moving) {
                _rigidbody.velocity = _directionsHandler.current * _speedHandler.speed;
                _speedHandler.IncreaseSpeed(deltaTime);
            }
            else
                _rigidbody.velocity = Vector3.zero;
        }
        #endregion // PRIVATE_METHODS
    }
}
