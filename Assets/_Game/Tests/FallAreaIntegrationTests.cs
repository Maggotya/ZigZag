using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Assets._Game.Scripts.Game;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Linq;
using Assets._Game.Scripts.Game.Objects.FallArea;

public class FallAreaIntegrationTests : MainSceneTestFixture
{
    private IFallArea _fallArea { get; set; }

    protected override IEnumerator LoadMainScene()
    {
        yield return base.LoadMainScene();
        _fallArea = SceneManager.GetActiveScene().GetRootGameObjects().First().GetComponentInChildren<IFallArea>();
    }

    [UnityTest]
    public IEnumerator SearchingFallArea_IFallAreaIsNotNull()
    {
        yield return LoadMainScene();

        Assert.IsNotNull(_fallArea);
    }

    [UnityTest]
    public IEnumerator WaitCatchedBall_For2Seconds_TimeoutIsNotReached()
    {
        yield return LoadMainScene();

        var catched = false;
        _fallArea.onObjectFallen.AddListener(
            () => catched = true);

        _gameManager.StartGame();
        _gameManager.ball.ChangeDirection();

        var timeout = 2f;
        var finishTime = Time.time + timeout;
        yield return new WaitUntil(() => catched || Time.time > finishTime);

        Assert.IsTrue(catched);
    }

    [UnityTest]
    public IEnumerator WaitCatchedBall_For2Seconds_GameIsOver()
    {
        yield return LoadMainScene();

        var catched = false;
        _fallArea.onObjectFallen.AddListener(
            () => catched = true);

        _gameManager.StartGame();
        _gameManager.ball.ChangeDirection();

        var timeout = 2f;
        var finishTime = Time.time + timeout;
        yield return new WaitUntil(() => catched || Time.time > finishTime);

        Assert.IsFalse(_gameManager.gameStarted);
    }

    [UnityTest]
    public IEnumerator LaunchBall_Wait_Restart_FallAreaInStartPosition()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return null;
        var startPosition = _fallArea.gameObject.transform.position;

        yield return new WaitForSeconds(1f);

        _gameManager.Restart();
        yield return null;
        var newPosition = _fallArea.gameObject.transform.position;

        var eps = 0.1f;
        Assert.AreEqual(startPosition.x, newPosition.x, eps);
        Assert.AreEqual(startPosition.y, newPosition.y, eps);
        Assert.AreEqual(startPosition.z, newPosition.z, eps);
    }

    [UnityTest]
    public IEnumerator EndGameByCatchingBall_Restart_FallAreaInStartPosition()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        _gameManager.ball.ChangeDirection();
        var startPosition = _fallArea.gameObject.transform.position;

        yield return new WaitWhile(() => _gameManager.gameStarted);

        _gameManager.Restart();
        yield return null;
        var newPosition = _fallArea.gameObject.transform.position;

        var eps = 0.1f;
        Assert.AreEqual(startPosition.x, newPosition.x, eps);
        Assert.AreEqual(startPosition.y, newPosition.y, eps);
        Assert.AreEqual(startPosition.z, newPosition.z, eps);
    }
}
