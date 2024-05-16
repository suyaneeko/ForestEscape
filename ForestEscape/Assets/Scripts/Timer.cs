using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    [SerializeField] private GameObject timePopUp;
    private float fTimer = 60f;
    private float fGoalTime = 0f;

    bool temp = true;

    void Start()
    {
        timePopUp.SetActive(false);
        timeText.text = "60";
    }

    void Update()
    {
        if(fGoalTime >= fTimer && temp)
        {
            timePopUp.SetActive(true);
            temp = false;
        }
        else
        {
            fTimer -= Time.deltaTime;
            timeText.text = Mathf.Round(fTimer).ToString();
            if (Mathf.Round(fTimer) <= 5f)
            {
                timeText.color = Color.red;
            }
        }

    }
}
