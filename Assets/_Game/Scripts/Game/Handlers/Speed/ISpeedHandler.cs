
using Assets._Game.Scripts.Game.Objects.Interfaces;

namespace Assets._Game.Scripts.Game.Handlers.Speed
{
    interface ISpeedHandler : IResetable
    {
        float speed { get; }

        void IncreaseSpeed(float deltaTime);
        void DicreaseSpeed(float deltaTime);
    }
}
