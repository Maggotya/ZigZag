using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Moving.Allower
{
    interface IMoveAllower
    {
        bool allowed { get; }
        BoolUnityEvent onStatusChanged { get; }
    }
}
