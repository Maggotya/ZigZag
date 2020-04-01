
namespace Assets._Game.Scripts.Game.Objects.Interfaces
{
    interface IActive
    {
        bool active { get; }

        void SetActive(bool status);
    }
}
