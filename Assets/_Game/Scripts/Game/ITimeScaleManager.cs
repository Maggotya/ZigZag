using Assets._Game.Scripts.Game.Objects.Interfaces;

namespace Assets._Game.Scripts.Game
{
    interface ITimeScaleManager : IResetable
    {
        void Start();
        void Stop();
        void EnableScale(bool status);
    }
}
