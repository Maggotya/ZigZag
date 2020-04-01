using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Game.Scripts.Game
{
    class GameManager : MonoBehaviour, IGameManager
    {
        #region PUBLIC_VALUES
        public bool gamePaused { get; private set; }
        public bool gameStarted { get; private set; }
        #endregion // PUBLIC_VALUES

        ///////////////////////////////

        #region PUBLIC_METHODS
        public void StartGame()
        {
            if (gameStarted)
                return;

            gameStarted = true;
        }

        public void EndGame()
        {
            if (gameStarted == false)
                return;

            gameStarted = false;
        }

        public void SetPause(bool status)
        {
            if (gamePaused == status)
                return;

            gamePaused = status;
            Time.timeScale = status ? 0 : 1;
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
    }
}