using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public List<GameObject> progressBar;
    private QuestManager _questManager;
    private int index;

    public void Start()
    {
        _questManager = FindAnyObjectByType<QuestManager>();

        foreach (GameObject item in progressBar)
        {
            item.SetActive(false);
        }
    }

    public void ChargeGem()
    {
        index = _questManager.GetIndex();
        progressBar[index].SetActive(true);
    }

    public void ResetProgress()
    {
        foreach (GameObject item in progressBar)
        {
            item.SetActive(false);
        }
    }
}
