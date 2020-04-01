using UnityEngine;

namespace Assets._Game.Scripts.Game.Handlers.Speed
{
    class SpeedHandler : ISpeedHandler
    {
        #region PUBLIC_FIELDS
        public float startSpeed { get; private set; }
        public float acceleration { get; private set; }
        public float minSpeed { get; private set; }
        public float maxSpeed { get; private set; }

        public float speed { get; private set; }
        #endregion // PUBLIC_FIELDS

        ////////////////////////////////////////////////

        #region CONTRUCTORS
        public SpeedHandler()
        {
            startSpeed = 0f;
            acceleration = 0f;
            minSpeed = 0f;
            maxSpeed = Mathf.Infinity;

            speed = startSpeed;
        }

        public SpeedHandler(float speed) : this()
            => this.speed = this.startSpeed = speed;

        public SpeedHandler(float startSpeed, float acceleration) : this(startSpeed)
            => this.acceleration = acceleration;

        public SpeedHandler(float startSpeed, float acceleration, float maxSpeed) : this(startSpeed, acceleration)
            => this.maxSpeed = maxSpeed;

        public SpeedHandler(float startSpeed, float acceleration, float minSpeed, float maxSpeed) : this(startSpeed, acceleration, maxSpeed)
            => this.minSpeed = minSpeed;
        #endregion // CONTRUCTORS

        #region PUBLIC_METHODS
        public void IncreaseSpeed(float deltaTime)
            => SetSpeed(speed + acceleration * deltaTime);

        public void DicreaseSpeed(float deltaTime)
            => SetSpeed(speed - acceleration * deltaTime);

        public void Reset()
            => SetSpeed(startSpeed);
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void SetSpeed(float speed)
            => this.speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        #endregion // PRIVATE_METHODS
    }
}
