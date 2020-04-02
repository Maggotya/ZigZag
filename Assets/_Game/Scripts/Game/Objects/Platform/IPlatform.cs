using System;
using Assets._Game.Scripts.Game.Objects.AnimatedObjects;
using Assets._Game.Scripts.Game.Objects.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Platform
{
    public interface IPlatform : IRemoteDestroyable, IAnimated
    {
        bool isActive { get; }
        bool isUsing { get; }
        int index { get; set; }

        Action<IPlatform> onStartBeUsing { get; set; }
        Action<IPlatform> onFinishBeUsing { get; set; }
        Action onDeactivating { get; set; }

        Vector3 positionToPlace { get; }

        void Appear(bool animated);
    }
}
