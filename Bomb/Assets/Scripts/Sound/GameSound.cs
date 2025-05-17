using Common;
using GameLogic;
using UnityEngine;

namespace Sound
{
    public class GameSound : GameObserverMonoBehaviour
    {

        [SerializeField]
        AudioSource playSource;
   
        [SerializeField]
        AudioSource backgroundTicksSource;
   
        [SerializeField]
        AudioSource explosionSource;

        [SerializeField]
        AudioSource alertSource;

        [SerializeField]
        AudioClip alertOnce;

        [SerializeField]
        AudioClip playStateSound;

        [SerializeField]
        AudioClip resultStateSound;

        float alertTimer = 0;
        bool isAlert = false;

        protected override void OnAlert()
        {
            isAlert = true;
            alertTimer = 5;
            backgroundTicksSource.Stop();
            alertSource.PlayOneShot(alertOnce);
        }

        void Update()
        {
            if (isAlert)
            {
                alertTimer -= Time.deltaTime;
                if (alertTimer <= 0)
                {
                    alertSource.Play();
                    alertTimer = 0;
                    isAlert = false;
                }
            }
        }

        void PlayClip(AudioSource audioSource, AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        // Update is called once per frame
        protected override void OnStateChanged(GameState state)
        {
            if (state == GameState.Play)
            {
                alertTimer = 0;
                PlayClip(playSource, playStateSound);
                backgroundTicksSource.loop = true;
                backgroundTicksSource.Play();
            }
            else if (state == GameState.Explosion)
            {
                alertSource.Stop();
                explosionSource.Play();
                backgroundTicksSource.Stop();

            }
            else if (state == GameState.Result)
            {
                PlayClip(playSource, resultStateSound);
                backgroundTicksSource.Stop();
            }
            else
            {
                backgroundTicksSource.Stop();
            }
        }
    }
}
