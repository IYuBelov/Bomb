using Common;
using GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
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

        protected override void OnStateChanged(GameState state)
        {
            if (state == GameState.Countdown)
            {
                timerIsRunning = true;
                timeRemaining = Constants.CountdownTime - 0.01f;
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
}
