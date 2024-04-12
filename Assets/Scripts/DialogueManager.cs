using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> dialogueQueue = new Queue<string>();
    private CharacterController2D _characterController2D;
    private PlayerInteraction _playerInteraction;
    private GameManager _gameManager;
    private LevelManager _levelManager;
    private bool hasWon = false;

    public TMP_Text dialogueText;
    public GameObject dialogue;

    private void Start()
    {
        _levelManager = FindAnyObjectByType<LevelManager>();
        _characterController2D = FindAnyObjectByType<CharacterController2D>();
        _playerInteraction = FindAnyObjectByType<PlayerInteraction>();
        _gameManager = GetComponentInParent<GameManager>();
        dialogue.SetActive(false);
    }

    public void StartDialogue(List<string> dialogueList, bool hasWon)
    {
        _gameManager.inDialogue = true;
        this.hasWon = hasWon;
        Cursor.lockState = CursorLockMode.None;
        _characterController2D.enabled = false;
        _playerInteraction.enabled = false;
        foreach (string dialogue in dialogueList)
        {
            dialogueQueue.Enqueue(dialogue);
        }
        NextDialogue();
        dialogue.SetActive(true);
    }

    public void NextDialogue()
    {
        if (dialogueQueue.Count <= 0)
        {
            EndDialogue();
            return;
        }
        dialogueText.text = dialogueQueue.Dequeue();
    }

    private void EndDialogue()
    {
        _gameManager.inDialogue = false;
        Cursor.lockState = CursorLockMode.Locked;
        _characterController2D.enabled = true;
        _playerInteraction.enabled = true;
        dialogue.SetActive(false);
        if (hasWon == true)
        {
            _levelManager.sceneChange = false;
            _levelManager.LoadScene("GameEnd");
        }
    }
}