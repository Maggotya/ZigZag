

using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Interfaces
{
    public interface IDirected
    {
        Vector3 direction { get; }
        void ChangeDirection();
    }
}
