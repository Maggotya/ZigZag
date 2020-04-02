using UnityEngine;

namespace Assets._Game.Scripts.Game
{
    public interface IGameManager
    {
        bool gamePaused { get; }
        bool gameStarted { get; }


        void StartGame();
        void EndGame();
        void SetPause(bool status);
        void Restart();
        void Quit();
    }
}