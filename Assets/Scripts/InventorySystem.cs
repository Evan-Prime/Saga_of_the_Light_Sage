using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public List<Image> itemSprites;
    private QuestManager _questManager;
    private int index;

    public void Start()
    {
        _questManager = FindAnyObjectByType<QuestManager>();

        foreach (Image item in itemSprites)
        {
            item.enabled = false;
        }
    }

    public void PickUpItem()
    {
        index = _questManager.GetIndex();
        itemSprites[index].enabled = true;
    }

    public void TurnInItem()
    {
        index = _questManager.GetIndex();
        itemSprites[index].enabled = false;
    }

    public void ResetInventory()
    {
        foreach (Image item in itemSprites)
        {
            item.enabled = false;
        }
    }

}
