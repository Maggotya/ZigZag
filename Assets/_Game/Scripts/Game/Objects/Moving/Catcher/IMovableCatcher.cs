using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.Moving.Catcher
{
    interface IMovableCatcher
    {
        bool inContact { get; }

        UnityEvent onBecomeBusy { get; set; }
        UnityEvent onBecomeFree { get; set; }
    }
}
