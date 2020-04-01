using Assets._Game.Scripts.Game.Objects.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform
{
    interface IPlatformGenerator : IActive
    {
        IPlatformUnityEvent onCreated { get; set; }

        void GenerateInitialPlatforms();
        Vector3 GetInitialPositionOn();
    }
}
