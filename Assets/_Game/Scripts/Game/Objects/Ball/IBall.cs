using Assets._Game.Scripts.Game.Objects.Interfaces;
using Assets._Game.Scripts.Game.Objects.Reset;

namespace Assets._Game.Scripts.Game.Objects.Ball
{
    interface IBall : IGameObjectHost, IMovable, IDirected, IPlacedOnSurface, IResetable
    {
    }
}
