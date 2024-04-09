using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq.Expressions;
using System.Threading;

public class InfoBox : MonoBehaviour
{
    private TMP_Text infoText;
    private float timer;

    public float timerValue = 2;

    private void Start()
    {
        infoText = GetComponent<TMP_Text>();
        infoText.enabled = false;
    }

    public void SetInfoText(string newText)
    {
        timer = 0;
        infoText.text = newText;
        infoText.enabled = true;
    }

    private void Update()
    {
        if (infoText.enabled)
        {
            timer += Time.deltaTime;
            if (timer >= timerValue)
            {
                infoText.enabled = false;
            }
        }
    }
}
