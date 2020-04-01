﻿using Assets._Game.Scripts.Game.Objects.Interfaces;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Scoring
{
    interface IScore : IResetable
    {
        int score { get; }
        IntUnityEvent onChanged { get; set; }

        void AddScore();
        void SubtractScore();
    }
}
