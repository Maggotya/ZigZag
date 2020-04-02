using Assets._Game.Scripts.Game.Objects.Interfaces;
using Assets._Game.Scripts.Game.Objects.Reset;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform
{
    public interface IPlatformGenerator : IActive, IResetable
    {
        IPlatformUnityEvent onCreated { get; set; }

        void GenerateInitialPlatforms();
        Vector3 GetInitialPositionOn();
    }
}
