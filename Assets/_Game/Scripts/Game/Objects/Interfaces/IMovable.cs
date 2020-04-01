
namespace Assets._Game.Scripts.Game.Objects.Interfaces
{
    interface IMovable
    {
        bool moving { get; }
        bool canMove { get; }

        void StartMove();
        void StopMove();
        void SetPermissionToMove(bool status);
    }
}
