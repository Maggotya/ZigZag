using Assets._Game.Scripts.Game.Objects.Reset;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Scoring
{
    public interface IScore : IResetable
    {
        int score { get; }
        IntUnityEvent onChanged { get; set; }

        void AddScore();
        void SubtractScore();
    }
}
