using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Changer
{
    interface IDirectionChanger
    {
        Vector3 currentDirection { get; }

        Vector3 ChangeDirection();
    }
}
