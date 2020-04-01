using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Interfaces
{
    interface IGamePrefabs
    {
        GameObject ball { get; }
        GameObject gem { get; }
        GameObject platform { get; }
    }
}
