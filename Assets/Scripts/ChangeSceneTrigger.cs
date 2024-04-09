using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    private LevelManager _levelManager;
    public string sceneName;
    public bool hasWon;

    private void Awake()
    {
        _levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _levelManager.gameWin = hasWon;
        _levelManager.LoadScene(sceneName);
    }
}
