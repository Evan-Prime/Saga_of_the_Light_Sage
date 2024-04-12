using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentInteractable;
    public InteractableObject interactableObjScript;

    private void Update()
    {
        if (currentInteractable != null && interactableObjScript != null && Input.GetKeyDown(KeyCode.E)) 
        {
            DoInteraction();
        }
    }

    public void DoInteraction()
    {
        switch (interactableObjScript.type)
        {
            case InteractableObject.ObjectType.Nothing:
                break;
            case InteractableObject.ObjectType.PickUp: 
                interactableObjScript.PickUp(currentInteractable); 
                break;
            case InteractableObject.ObjectType.Info:
                interactableObjScript.Info();
                break;
            case InteractableObject.ObjectType.Dialogue:
                interactableObjScript.Dialogue();
                break;
            case InteractableObject.ObjectType.Gate: 
                interactableObjScript.Gate(); 
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable") == true || collision.CompareTag("QuestNPC") == true || collision.CompareTag("QuestItem") == true)
        {
            currentInteractable = collision.gameObject;
            interactableObjScript = collision.GetComponent<InteractableObject>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable") == true || collision.CompareTag("QuestNPC") == true || collision.CompareTag("QuestItem") == true)
        {
            currentInteractable = null;
            interactableObjScript = null;
        }
    }
}
