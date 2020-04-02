
namespace Assets._Game.Scripts.Game.Objects.Interfaces
{
    public interface IActive
    {
        bool active { get; }

        void SetActive(bool status);
    }
}
