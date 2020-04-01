using System;

namespace Assets._Game.Scripts.Game.Objects.Interfaces
{
    interface IRemoteDestroyable : IGameObjectHost
    {
        bool needDestroy { get; }
        Action<IRemoteDestroyable> onBecomeDestroyable { get; set; }

        void SetDestroyed();
    }
}
