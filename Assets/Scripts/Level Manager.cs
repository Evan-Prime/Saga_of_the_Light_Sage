using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    private GameManager _gameManager;
    public bool gameWin;

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
            if (sceneToLoad.StartsWith("Gameplay"))
            {
                _gameManager.gameState = GameManager.GameState.Gameplay;
            }
            if (sceneToLoad == "GameEnd")
            {
                if (gameWin == true)
                {
                    _gameManager.gameState = GameManager.GameState.GameWin;
                }
                else if (gameWin == false)
                {
                    _gameManager.gameState = GameManager.GameState.GameOver;
                }
                
            }
        }


        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _gameManager.MovePlayerToSpawnPosition();
    }
}
