using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    
    private LevelManager _levelManager;
    private UIManager _uiManager;
    private CharacterController2D _characterController2D;
    private ProgressBar _progressBar;
    private InventorySystem _inventorySystem;
    private QuestManager _questManager;

    public GameObject spawnPoint;
    public GameObject[] transitionPoints;
    public GameObject player;
    public GameObject playerArt;
    public bool inDialogue;

    public enum GameState { MainMenu, Gameplay, Credit, Paused, GameOver, GameWin }
    public GameState gameState;
    private GameState lastGameState;
    private GameState returnFromCredits;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;

        gameState = GameState.MainMenu;

        _levelManager = FindAnyObjectByType<LevelManager>();

        _uiManager = FindAnyObjectByType<UIManager>();

        _characterController2D = FindAnyObjectByType<CharacterController2D>();

        _progressBar = FindAnyObjectByType<ProgressBar>();

        _inventorySystem = FindAnyObjectByType<InventorySystem>();

        _questManager = FindAnyObjectByType<QuestManager>();
    }

    private void Start()
    {
        if (instance != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.MainMenu:    MainMenu(); break;
            case GameState.Gameplay:    Gameplay(); break;
            case GameState.Credit:     Credit(); break;
            case GameState.Paused:      Paused(); break;
            case GameState.GameWin:     GameWin(); break;
        }
    }

    private void MainMenu()
    {
        Cursor.lockState = CursorLockMode.None;

        _progressBar.ResetProgress();
        _inventorySystem.ResetInventory();
        _questManager.ResetQuests();
        _levelManager.ResetLevel();

        playerArt.SetActive(false);
        _characterController2D.enabled = false;

        _uiManager.UIMainManu();
    }

    private void Gameplay()
    {
        if (!inDialogue)
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            _characterController2D.enabled = true;
        }
        else
        {
            _characterController2D.Freeze();
            _characterController2D.enabled = false;
        }

        playerArt.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lastGameState = gameState;
            gameState = GameState.Paused;
        }

        _uiManager.UIGameplay();
    }

    private void Credit()
    {
        Cursor.lockState = CursorLockMode.None;

        _characterController2D.enabled = false;

        if (Input.GetKeyDown(KeyCode.Escape) && returnFromCredits != GameState.MainMenu)
        {
            gameState = GameState.Paused;
        }

        _uiManager.UICredit();
    }

    private void Paused()
    {
        Cursor.lockState = CursorLockMode.None;

        _characterController2D.enabled = false;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = lastGameState;
        }

        _uiManager.UIPaused();
    }

    private void GameWin()
    {
        Cursor.lockState = CursorLockMode.None;

        playerArt.SetActive(false);
        _characterController2D.enabled = false;

        _uiManager.UIGameWin();
    }

    public void QuitGame()
    {
        //Debug line to test quit function in editor
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void MovePlayerToSpawnPosition()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        player.transform.position = spawnPoint.transform.position;
    }

    public void MovePlayerToTransitionPosition()
    {
        transitionPoints = GameObject.FindGameObjectsWithTag("Transition");
        foreach (GameObject transitionPoint in transitionPoints)
        {
            if (transitionPoint.GetComponent<ChangeSceneTrigger>().sceneName == _levelManager.previousSceneName)
            {
                player.transform.position = transitionPoint.transform.position;
            }
        }
    }

    public void ToCredits()
    {
        returnFromCredits = gameState;
        gameState = GameState.Credit;
    }

    public void FromCredits()
    {
        gameState = returnFromCredits;
    }

    public void ResumeGame()
    {
        gameState = lastGameState;
    }
}
