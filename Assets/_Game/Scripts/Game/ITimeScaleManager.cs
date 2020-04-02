﻿using Assets._Game.Scripts.Game.Objects.Reset;

namespace Assets._Game.Scripts.Game
{
    public interface ITimeScaleManager : IResetable
    {
        void Start();
        void Stop();
        void EnableScale(bool status);
    }
}
