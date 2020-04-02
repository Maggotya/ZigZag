using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Assets._Game.Scripts.Game;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Linq;

public class MainSceneTestFixture : SceneTestFixture
{
    protected IGameManager _gameManager { get; set; }

    private const string SCENE_NAME = "MainScene";

    protected virtual IEnumerator LoadMainScene()
    {
        yield return LoadScene(SCENE_NAME);
        _gameManager = SceneManager.GetActiveScene().GetRootGameObjects().First().GetComponent<IGameManager>();
    }
}
