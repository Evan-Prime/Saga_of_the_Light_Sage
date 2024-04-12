using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Nothing, Info, PickUp, Dialogue, Gate}
    public ObjectType type;
    public int step;
    public bool hasWon = false;

    [Header("Info Text")]
    public string info;

    [Header("Start Quest")]
    public List<string> dialogue1;

    [Header("During Quest")]
    public List<string> dialogue2;

    [Header("End Quest")]
    public List<string> dialogue3;

    [Header("After Quest")]
    public List<string> dialogue4;

    [Header("Gate")]
    public int gateID;
    public int[] unlockCode = new int[5];

    private InfoBox box;
    private DialogueManager dManager;
    private QuestManager _questManager;
    private InventorySystem _inventorySystem;
    private ProgressBar _progressBar;

    private void Start()
    {
        _progressBar = FindAnyObjectByType<ProgressBar>();
        _inventorySystem = FindAnyObjectByType<InventorySystem>();
        box = FindObjectOfType<InfoBox>();
        dManager = FindObjectOfType<DialogueManager>();
        _questManager = FindObjectOfType<QuestManager>();
        if (_questManager.gateIDs.Contains(gateID) && gateID != 0)
        {
            gameObject.SetActive(false);
        }

    }

    public void Info()
    {
        box.SetInfoText(info);
    }

    public void PickUp(GameObject Interactable)
    {
        Interactable.gameObject.SetActive(false);
        _inventorySystem.PickUpItem();
        if (hasWon == false)
        {
            _questManager.NextQuestStep();
        }
    }

    public void Dialogue()
    {
        switch (step)
        {
            case 0:
                dManager.StartDialogue(dialogue1, hasWon);
                if (hasWon == false)
                {
                    _questManager.NextQuestStep();
                    _questManager.questItem.SetActive(true);
                }
                break;
            case 1:
                dManager.StartDialogue(dialogue2, hasWon);
                break;
            case 2:
                dManager.StartDialogue(dialogue3, hasWon);
                if (hasWon == false)
                {
                    _inventorySystem.TurnInItem();
                    _questManager.NextQuestStep();
                    _progressBar.ChargeGem();
                }
                break;
            case 3:
                dManager.StartDialogue(dialogue4, hasWon);
                break;
        }
        
    }

    public void Gate()
    {
        for(int i = 0; i < unlockCode.Length; i++)
        {
            if (_questManager.questSteps[i] < unlockCode[i])
            {
                return;
            }
        }
        _questManager.gateIDs.Add(gateID);
        gameObject.SetActive(false);
    }
}