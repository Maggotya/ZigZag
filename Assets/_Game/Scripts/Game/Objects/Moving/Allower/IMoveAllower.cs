using Assets._Game.Scripts.Game.Objects.Reset;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Moving.Allower
{
    interface IMoveAllower : IResetable
    {
        bool allowed { get; }
        BoolUnityEvent onStatusChanged { get; }
    }
}
