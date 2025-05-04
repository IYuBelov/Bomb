using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : GameObserverMonoBehaviour
{
    Text textComponent;
    
    [SerializeField]
    AudioSource playaudio;

    float timeRemaining = 0;
    bool timerIsRunning = false;
    // Start is called before the first frame update
    protected override void Start()
    {   
        textComponent = GetComponent<Text>();
        base.Start();
    }

    protected override void UpdateState(GameState state)
    {
        if (state == GameState.COUNTDOWN)
        {
            timerIsRunning = true;
            timeRemaining = Constants.COUNTDOWN_TIME - 0.01f;
            UpdateTime(timeRemaining);
        }
        else
        {
            timerIsRunning = false;
            textComponent.text = "";
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining > 0)
            {
                UpdateTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay);
        textComponent.text = string.Format("{0:0}", seconds);
    }

    void UpdateTime(float timeToDisplay)
    {
        var oldText = textComponent.text;
        DisplayTime(timeToDisplay);
        if (oldText != textComponent.text)
        {
            playaudio.Play();
        }
    }
}
