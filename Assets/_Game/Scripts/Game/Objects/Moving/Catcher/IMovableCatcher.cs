using Assets._Game.Scripts.Game.Objects.Reset;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Moving.Catcher
{
    interface IMovableCatcher : IResetable
    {
        bool inContact { get; }

        UnityEvent onBecomeBusy { get; set; }
        UnityEvent onBecomeFree { get; set; }
    }
}
