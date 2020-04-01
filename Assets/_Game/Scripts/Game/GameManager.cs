using Assets._Game.Scripts.Game.Objects.Ball;
using Assets._Game.Scripts.Game.Objects.Generating.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Platform;
using Assets._Game.Scripts.Game.Save;
using Assets._Game.Scripts.Game.Scoring;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets._Game.Scripts.Game
{
    class GameManager : MonoBehaviour, IGameManager
    {
        #region PRIVATE_VALUES
        [Inject] private IBall _ball { get; set; }
        [Inject] private IPlatformGenerator _platformGenerator { get; set; }
        [Inject] private IGemGenerator _gemGenerator { get; set; }

        [Inject] private IScore _score { get; set; }
        [Inject] private ISaveManager _saveManager { get; set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool gamePaused { get; private set; }
        public bool gameStarted { get; private set; }
        #endregion // PUBLIC_VALUES

        #region CONSTS
        private const int PAUSED_SCALE = 0;
        private const int UNPAUSED_SCALE = 1;
        #endregion // CONSTS

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

            _platformGenerator.SetActive(true);
            _ball.StartMove();
        }

        public void EndGame()
        {
            if (gameStarted == false)
                return;

            gameStarted = false;
            SaveGame();

            _platformGenerator.SetActive(false);
            _ball.StopMove();
        }

        public void SetPause(bool status)
        {
            if (gamePaused == status)
                return;

            gamePaused = status;
            Time.timeScale = status ? PAUSED_SCALE : UNPAUSED_SCALE;
        }

        public void Restart()
            => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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
            Time.timeScale = UNPAUSED_SCALE;

            _platformGenerator.GenerateInitialPlatforms();
            _ball.SetPositionOnSurface(_platformGenerator.GetInitialPositionOn());
        }
        private void SaveGame()
        {
            if (_saveManager.lastSave.bestScore < _score.score)
                _saveManager.Save(_score.score);
        }
        #endregion // PRIVATE_METHODS
    }
}