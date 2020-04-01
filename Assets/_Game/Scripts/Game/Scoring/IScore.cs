using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Scoring
{
    interface IScore
    {
        int score { get; }
        IntUnityEvent onChanged { get; set; }

        void AddScore();
        void SubtractScore();
    }
}
