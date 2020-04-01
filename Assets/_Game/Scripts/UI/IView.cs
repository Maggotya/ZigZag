using Assets._Game.Scripts.Game.Objects.Interfaces;

namespace Assets._Game.Scripts.UI
{
    interface IView : IActive
    {
        void Enable();
        void Disable();
    }
}
