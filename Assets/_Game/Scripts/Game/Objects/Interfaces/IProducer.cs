
namespace Assets._Game.Scripts.Game.Objects.Interfaces
{
    interface IProducer<T>
    {
        T Produce();
        void HandleUnused(T booster);
        void HandleUnused(T[] boosters);
    }
}
