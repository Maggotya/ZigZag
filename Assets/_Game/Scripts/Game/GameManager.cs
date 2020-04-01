using Assets._Game.Scripts.Game.Objects.Ball;
using Assets._Game.Scripts.Game.Objects.Generating.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Platform;
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
        [Inject] private IBall _ball { get; set; }
        [Inject] private CinemachineVirtualCamera _camera { get; set; }
        [Inject] private IPlatformGenerator _platformGenerator { get; set; }
        [Inject] private IGemGenerator _gemGenerator { get; set; }

        [Inject] private IUiManager _uiManager { get; set; }
        [Inject] private IScore _score { get; set; }
        [Inject] private ISaveManager _saveManager { get; set; }
        [Inject] private ITimeScaleManager _timeScale { get; set; }
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
            _uiManager.LaunchScoreView();

            _platformGenerator.SetActive(true);
            _ball.StartMove();
        }

        public void EndGame()
        {
            if (gameStarted == false)
                return;

            gameStarted = false;
            SaveGame();

            _camera.enabled = false;
            _platformGenerator.SetActive(false);
            _ball.StopMove();

            _uiManager.LaunchResultView(_score.score, _saveManager.lastSave.bestScore);
        }

        public void SetPause(bool status)
        {
            if (gamePaused == status)
                return;

            gamePaused = status;
            _timeScale.EnableScale(!status);

            if (status) _uiManager.LaunchPauseView();
            else _uiManager.LaunchScoreView();
        }

        public void Restart()
        {
            _ball.Reset();
            _score.Reset();
            _platformGenerator.Reset();
            _gemGenerator.Reset();
            _timeScale.Reset();

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

            _timeScale.Start();
            _camera.enabled = true;
            _platformGenerator.GenerateInitialPlatforms();
            _ball.SetPositionOnSurface(_platformGenerator.GetInitialPositionOn());

            _uiManager.LaunchStartView();
        }

        private void SaveGame()
        {
            if (_saveManager.lastSave.bestScore < _score.score)
                _saveManager.Save(_score.score);
        }
        #endregion // PRIVATE_METHODS
    }
}