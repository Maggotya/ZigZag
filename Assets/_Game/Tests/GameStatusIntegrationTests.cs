using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Assets._Game.Scripts.Game;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameStatusIntegrationTests : MainSceneTestFixture
{
    [UnityTest]
    public IEnumerator SerachingInScene_IGameManagerIsNotNull()
    {
        yield return LoadMainScene();
        Assert.IsNotNull(_gameManager);
    }

    #region START_AND_END
    [UnityTest]
    public IEnumerator StartGame_GameStatusIsTrue()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        Assert.IsTrue(_gameManager.gameStarted);
    }

    [UnityTest]
    public IEnumerator EndGameAfterStart_GameStatusIsFalse()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        _gameManager.EndGame();
        yield return null;

        Assert.IsFalse(_gameManager.gameStarted);
    }
    #endregion // START_AND_END

    #region RESTART
    [UnityTest]
    public IEnumerator RestartGameAfterStart_GameStatusIsFalse()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        _gameManager.Restart();
        yield return null;

        Assert.IsFalse(_gameManager.gameStarted);
    }

    [UnityTest]
    public IEnumerator StartGameAfterRestart_GameStatusIsTrue()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        _gameManager.Restart();
        yield return null;
        _gameManager.StartGame();
        yield return null;

        Assert.IsTrue(_gameManager.gameStarted);
    }

    [UnityTest]
    public IEnumerator RestartGameAfterEnd_GameStatusIsFalse()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        _gameManager.EndGame();
        yield return null;
        _gameManager.Restart();
        yield return null;

        Assert.IsFalse(_gameManager.gameStarted);
    }

    [UnityTest]
    public IEnumerator StartGameAfterRestartedEnd_GameStatusIsTrue()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        _gameManager.EndGame();
        yield return null;
        _gameManager.Restart();
        yield return null;
        _gameManager.StartGame();
        yield return null;

        Assert.IsTrue(_gameManager.gameStarted);
    }
    #endregion // RESTART

    #region PAUSE_STATUS
    [UnityTest]
    public IEnumerator PauseGame_PauseStatusIsTrue()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        _gameManager.SetPause(true);

        Assert.IsTrue(_gameManager.gamePaused);
    }

    [UnityTest]
    public IEnumerator ContinueAfterPauseGame_PauseStatusIsFalse()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        _gameManager.SetPause(true);
        yield return null;
        _gameManager.SetPause(false);

        Assert.IsFalse(_gameManager.gamePaused);
    }

    [UnityTest]
    public IEnumerator RestartWhilePause_PauseStatusIsFalse()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        _gameManager.SetPause(true);
        yield return null;
        _gameManager.Restart();
        yield return null;

        Assert.IsFalse(_gameManager.gamePaused);
    }
    #endregion // PAUSE_STATUS
}