using System;
using Common;
using GameLogic;
using UnityEngine;

namespace Sound
{
    public class GameSound : GameObserverMonoBehaviour
    {

        [SerializeField]
        AudioSource audioSource;

        [SerializeField]
        AudioClip countdownClip;

        [SerializeField]
        AudioClip playClip;
        
        [SerializeField]
        AudioClip explosionClip;
        
        protected override void Start()
        {
            base.Start();
            _eventListener.Add(Events.EvCountDownTickChanged, new Action<int>(OnCountDownTickChanged));
        }

        void OnCountDownTickChanged(int count)
        {
            audioSource.PlayOneShot(countdownClip);
        }  
        
        protected override void OnStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Play:
                    audioSource.PlayOneShot(playClip);
                    break;
                case GameState.Explosion:
                    audioSource.PlayOneShot(explosionClip);
                    break;
            }
        }
        
        void PlayOnceShot(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Play();
        }
        
    }
}
