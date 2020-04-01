using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Input
{
    interface IInputManager
    {
        UnityEvent onGameKeyPress { get; }
    }
}
