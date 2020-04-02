using Assets._Game.Scripts.Game.Objects.Ball;
using Assets._Game.Scripts.Game.Objects.Generating.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Platform;
using Assets._Game.Scripts.Game.Objects.Interfaces;
using Assets._Game.Scripts.Game.Save;
using Assets._Game.Scripts.Game.Scoring;
using Assets._Game.Scripts.UI.Manager;
using Cinemachine;
using UnityEngine;

namespace Assets._Game.Scripts.Game
{
    public interface IGameManager
    {
        IBall ball { get; }
        CinemachineVirtualCamera camera { get; }
        IPlatformGenerator platformGenerator { get; }
        IGemGenerator gemGenerator { get; }

        IUiManager uiManager { get; }
        IScore score { get; }
        ISaveManager saveManager { get; }
        ITimeScaleManager timeScale { get; }

        bool gamePaused { get; }
        bool gameStarted { get; }


        void StartGame();
        void EndGame();
        void SetPause(bool status);
        void Restart();
        void Quit();
    }
}