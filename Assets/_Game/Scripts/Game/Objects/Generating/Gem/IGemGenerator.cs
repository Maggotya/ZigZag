using Assets._Game.Scripts.Game.Objects.Interfaces;
using Assets._Game.Scripts.Game.Objects.Platform;
using Assets._Game.Scripts.Game.Objects.Reset;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Generating.Gem
{
    interface IGemGenerator : IActive, IResetable
    {
        UnityEvent onCollected { get; set; }
        void TryGenerate(IPlatform platform);
    }
}
