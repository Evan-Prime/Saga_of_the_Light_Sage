using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    private GameManager _gameManager;
    public bool sceneChange;
    public string currentSceneName;
    public string previousSceneName;
    public bool canTransition = true;

    public void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if(sceneToLoad != null)
        {
            if (sceneToLoad == "MainMenu")
            {
                _gameManager.gameState = GameManager.GameState.MainMenu;
            }
            if (sceneToLoad.EndsWith("Scene"))
            {
                _gameManager.gameState = GameManager.GameState.Gameplay;
            }
            if (sceneToLoad == "GameEnd")
            {
                _gameManager.gameState = GameManager.GameState.GameWin;

            }
        }


        if (canTransition == true)
        {
            previousSceneName = currentSceneName;
            currentSceneName = sceneToLoad;
            SceneManager.LoadScene(sceneToLoad);
            canTransition = false;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneChange == false)
        {
            _gameManager.MovePlayerToSpawnPosition();
            canTransition = true;
        }
        else if (sceneChange == true)
        {
            _gameManager.MovePlayerToTransitionPosition();
        }
    }

    public void ResetLevel()
    {
        sceneChange = false;
    }
}
