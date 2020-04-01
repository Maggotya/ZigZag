using UnityEngine;

namespace Assets._Game.Scripts.Game
{
    class TimeScaleManager : ITimeScaleManager
    {
        #region CONSTS
        private const int STOPPED_SCALE = 0;
        private const int ACTION_SCALE = 1;
        #endregion // CONSTS

        #region PUBLIC_MEHODS
        public void Start()
            => EnableScale(true);

        public void Stop()
            => EnableScale(false);

        public void EnableScale(bool status)
            => Time.timeScale = status ? ACTION_SCALE : STOPPED_SCALE;

        public void Reset()
            => Start();
        #endregion // PUBLIC_MEHODS
    }
}
