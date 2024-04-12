using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public string[] questSceneNames;
    private LevelManager _levelManager;
    public int[] questSteps;
    public List<int> gateIDs;
    public GameObject questNPC;
    public GameObject questItem;
    private int index;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        _levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (questSceneNames.Contains(_levelManager.currentSceneName))
        {
            index = Array.IndexOf(questSceneNames, _levelManager.currentSceneName);
            questNPC = GameObject.FindGameObjectWithTag("QuestNPC");
            questItem = GameObject.FindGameObjectWithTag("QuestItem");
            if (questNPC != null)
            {
                questNPC.GetComponent<InteractableObject>().step = questSteps[index];
            }
            if (questItem  != null )
            {
                questItem.GetComponent<InteractableObject>().step = questSteps[index];
                if (questSteps[index] != 1)
                {
                    questItem.SetActive(false);
                }
            }
        }
    }

    public void NextQuestStep()
    {
        questSteps[index] += 1;
        questNPC.GetComponent<InteractableObject>().step = questSteps[index];
        questItem.GetComponent<InteractableObject>().step = questSteps[index];
    }

    public int GetIndex()
    {
        return index;
    }

    public void ResetQuests()
    {
        gateIDs.Clear();
        for (int i = 0; i < questSteps.Length; i++)
        {
            questSteps[i] = 0;
        }
    }
}
