using System;
using Assets._Game.Scripts.Game.Objects.AnimatedObjects;
using Assets._Game.Scripts.Game.Objects.Interfaces;

namespace Assets._Game.Scripts.Game.Objects.Gem
{
    interface IGem : IAnimated, IRemoteDestroyable, IPlacedOnSurface
    {
        Action<IGem> onCollected { get; set; }

        void Collect();
        void Deactivate();
    }
}
