
namespace Assets._Game.Scripts.Game.Handlers.Speed
{
    interface ISpeedHandler
    {
        float speed { get; }

        void IncreaseSpeed(float deltaTime);
        void DicreaseSpeed(float deltaTime);
        void Reset();
    }
}
