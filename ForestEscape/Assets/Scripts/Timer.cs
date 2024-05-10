using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    [SerializeField] private GameObject timePopUp;
    private float fTimer = 10f;
    private float fGoalTime = 0f;

    void Start()
    {
        timePopUp.SetActive(false);
        timeText.text = "10";
    }

    void Update()
    {
        if(fGoalTime >= fTimer)
        {
            timePopUp.SetActive(true);
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
