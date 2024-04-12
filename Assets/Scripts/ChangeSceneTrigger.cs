using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    private LevelManager _levelManager;
    private bool hasChanged = true;
    public string sceneName;
    public bool hasWon;

    private void Awake()
    {
        _levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _levelManager.sceneChange = hasChanged;
        _levelManager.LoadScene(sceneName);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_levelManager.currentSceneName != sceneName)
        {

            _levelManager.canTransition = true;
        }

    }
}