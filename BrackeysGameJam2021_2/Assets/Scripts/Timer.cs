using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeRemainig;
    public bool timerIsRunning;
    public TextMeshProUGUI timer;
    void Start()
    {
        timer.color = new Color32(255,255, 255,255);
        timeRemainig = 0;
        DisplayTimer();
        timerIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemainig > 0)
        {
            timeRemainig -= Time.deltaTime;
            DisplayTimer();
        }
        else
        {
            timeRemainig = 0;
            DisplayTimer();
            timerIsRunning = false;
        }
    }

    void DisplayTimer()
    {
        float minutes = Mathf.FloorToInt(timeRemainig / 60);
        float seconds = Mathf.FloorToInt(timeRemainig % 60);
        if (minutes == 0 && seconds <= 20)
            timer.color = new Color32(255, 0, 0, 255);
        if (minutes == 0 && seconds == 0)
            timer.text = "0:00";
        else if (minutes == 0 && seconds < 10)
            timer.text = "0:0" + seconds;
        else if (minutes < 10)
            timer.text = "0" + minutes + ":" + seconds;
        else
            timer.text = minutes + ":" + seconds;
    }

    public void startTimer(float time)
    {
        timeRemainig = time;
        timerIsRunning = true;
    }
}
