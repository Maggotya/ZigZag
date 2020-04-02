using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Assets._Game.Scripts.Game;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Linq;

public class BallStatusIntegrationTests : MainSceneTestFixture
{
    [UnityTest]
    public IEnumerator GettingBallInsideGameManager_BallISNotNull()
    {
        yield return LoadMainScene();
        Assert.IsNotNull(_gameManager.ball);
    }

    [UnityTest]
    public IEnumerator StartGame_BallIsMoving()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return new WaitForFixedUpdate();

        Assert.IsTrue(_gameManager.ball.moving);
        Assert.IsTrue(_gameManager.ball.canMove);
    }

    [UnityTest]
    public IEnumerator ChangeDirection_BallHasAnotherDirection()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        var startDirection = _gameManager.ball.direction;
        yield return new WaitForFixedUpdate();
        _gameManager.ball.ChangeDirection();
        var newDirection = _gameManager.ball.direction;
        yield return new WaitForFixedUpdate();

        Assert.AreNotEqual(startDirection, newDirection);
    }

    [UnityTest]
    public IEnumerator GameOver_BallCantMove()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return new WaitWhile(() => _gameManager.gameStarted);

        Assert.IsFalse(_gameManager.ball.canMove);
    }

    [UnityTest]
    public IEnumerator NewGameAfterLose_BallIsMoving()
    {
        yield return LoadMainScene();

        _gameManager.StartGame();
        yield return new WaitWhile(() => _gameManager.gameStarted);
        _gameManager.Restart();
        yield return null;
        _gameManager.StartGame();
        yield return new WaitForFixedUpdate();

        Assert.IsTrue(_gameManager.ball.moving);
        Assert.IsTrue(_gameManager.ball.canMove);
    }
}
