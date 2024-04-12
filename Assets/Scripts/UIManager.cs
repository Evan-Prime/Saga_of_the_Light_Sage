using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject gameplayUI;
    public GameObject creditUI;
    public GameObject pausedUI;
    public GameObject gameWinUI;

    public void UIMainManu()
    {
        mainMenuUI.SetActive(true);
        gameplayUI.SetActive(false);
        creditUI.SetActive(false);
        pausedUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    public void UIGameplay()
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        creditUI.SetActive(false);
        pausedUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    public void UICredit()
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(false);
        creditUI.SetActive(true);
        pausedUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    public void UIPaused()
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(false);
        creditUI.SetActive(false);
        pausedUI.SetActive(true);
        gameWinUI.SetActive(false);
    }
    

    public void UIGameWin()
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(false);
        creditUI.SetActive(false);
        pausedUI.SetActive(false);
        gameWinUI.SetActive(true);
    }
}
