using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Nothing, Info, PickUp, Dialogue }
    public ObjectType type;
    public string info;
    public List<string> dialogue;

    private InfoBox box;
    private DialogueManager dManager;

    private void Start()
    {
        box = FindObjectOfType<InfoBox>();
        dManager = FindObjectOfType<DialogueManager>();
    }

    public void Info()
    {
        box.SetInfoText(info);
    }

    public void PickUp(GameObject Interactable)
    {
        Debug.Log("You picked up a " + Interactable.name);
        Interactable.gameObject.SetActive(false);
    }

    public void Dialogue()
    {
        dManager.StartDialogue(dialogue);
    }
}