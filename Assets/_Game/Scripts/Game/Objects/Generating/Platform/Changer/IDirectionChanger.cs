using Assets._Game.Scripts.Game.Objects.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Changer
{
    interface IDirectionChanger : IResetable
    {
        Vector3 currentDirection { get; }

        Vector3 ChangeDirection();
    }
}
