using UnityEngine;

namespace Assets._Game.Scripts.Game.Handlers.Direction
{
    interface IDirectionHandler
    {
        Vector3 current { get; }
        Vector3 next { get; }
        Vector3 previous { get; }

        Vector3 MoveNext();
        Vector3 MovePrevious();
    }
}
