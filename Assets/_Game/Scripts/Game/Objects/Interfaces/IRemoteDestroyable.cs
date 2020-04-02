using System;

namespace Assets._Game.Scripts.Game.Objects.Interfaces
{
    public interface IRemoteDestroyable : IGameObjectHost
    {
        bool needDestroy { get; }
        Action<IRemoteDestroyable> onBecomeDestroyable { get; set; }

        void SetDestroyed();
    }
}
