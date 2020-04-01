using UnityEngine;

namespace Assets._Game.Scripts.Instruments.Following
{
    interface IPositionFollower
    {
        Transform followedObject { get; }
        Vector3 movingMask { get; }
        bool useOffset { get; }
        Vector3 offset { get; }

        void SetFollowedObject(Transform transform);
        void SetOffset(Vector3 offset, bool status);
        void SetOffset(Vector3 offset);
        void SetOffset(bool status);
    }
}
