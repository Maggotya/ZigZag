using Assets._Game.Scripts.Game.Objects.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.FallArea
{
    public interface IFallArea : IGameObjectHost
    {
        GameObject objectMustFall { get; }
        UnityEvent onObjectFallen { get; }
    }
}
