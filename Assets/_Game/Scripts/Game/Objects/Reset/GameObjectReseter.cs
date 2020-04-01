using Assets._Game.Scripts.Game.Objects.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Reset
{
    class GameObjectReseter : MonoBehaviour, IGameObjectReseter
    {
        public void Reset()
        {
            foreach (var component in GetComponents<IResetable>())
                component.Reset();
        }
    }
}
