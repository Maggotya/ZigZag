using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Moving.Surface
{
    [RequireComponent(typeof(Collider))]
    class SurfaceToMove : MonoBehaviour, ISurfaceToMove
    {
        [SerializeField] private bool _AllowMove;

        public bool allowMove => _AllowMove;
    }
}
