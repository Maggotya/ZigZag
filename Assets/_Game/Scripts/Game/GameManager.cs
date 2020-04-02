using Assets._Game.Scripts.Game.Objects.Ball;
using Assets._Game.Scripts.Game.Objects.Generating.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Platform;
using Assets._Game.Scripts.Game.Objects.Reset;
using Assets._Game.Scripts.Game.Save;
using Assets._Game.Scripts.Game.Scoring;
using Assets._Game.Scripts.UI.Manager;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets._Game.Scripts.Game
{
    class GameManager : MonoBehaviour, IGameManager
    {
        #region PRIVATE_VALUES
        [Inject] public IBall ball { get; private set; }
        [Inject] public new CinemachineVirtualCamera camera { get; private set; }
        [Inject] public IPlatformGenerator platformGenerator { get; private set; }
        [Inject] public IGemGenerator gemGenerator { get; private set; }
                 
        [Inject] public IUiManager uiManager { get; private set; }
        [Inject] public IScore score { get; private set; }
        [Inject] public ISaveManager saveManager { get; private set; }
        [Inject] public ITimeScaleManager timeScale { get; private set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool gamePaused { get; private set; }
        public bool gameStarted { get; private set; }
        #endregion // PUBLIC_VALUES

        //////////////////////////////////////////////

        #region MONO_BEHAVIOUR
        private void Start()
            => Initialize();
        #endregion // MONO_BEHAVIOUR

        #region PUBLIC_METHODS
        public void StartGame()
        {
            if (gameStarted)
                return;

            gameStarted = true;
            uiManager.LaunchScoreView();

            platformGenerator.SetActive(true);
            ball.StartMove();
        }

        public void EndGame()
        {
            if (gameStarted == false)
                return;

            gameStarted = false;
            SaveGame();

            camera.enabled = false;
            platformGenerator.SetActive(false);
            ball.StopMove();

            uiManager.LaunchResultView(score.score, saveManager.lastSave.bestScore);
        }

        public void SetPause(bool status)
        {
            if (gamePaused == status)
                return;

            gamePaused = status;
            timeScale.EnableScale(!status);

            if (status) uiManager.LaunchPauseView();
            else uiManager.LaunchScoreView();
        }

        public void Restart()
        {
            foreach (var obj in FindObjectsOfType<GameObjectReseter>())
                obj.Reset();

            timeScale.Reset();

            Initialize();
        }

        public void Quit()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void Initialize()
        {
            gameStarted = false;
            gamePaused = false;

            timeScale.Start();
            camera.enabled = true;
            platformGenerator.GenerateInitialPlatforms();
            ball.SetPositionOnSurface(platformGenerator.GetInitialPositionOn());

            uiManager.LaunchStartView();
        }

        private void SaveGame()
        {
            if (saveManager.lastSave.bestScore < score.score)
                saveManager.Save(score.score);
        }
        #endregion // PRIVATE_METHODS
    }
}