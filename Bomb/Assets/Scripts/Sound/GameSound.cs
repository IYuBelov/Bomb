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
        
        [SerializeField]
        AudioSource audioTickSource;
        
        [SerializeField]
        AudioClip tickClip1;        
      
        [SerializeField]
        AudioClip tickClip2;
        
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
                    PlayTick(tickClip1);
                    break;
                case GameState.Explosion:
                    audioSource.PlayOneShot(explosionClip);
                    audioTickSource.Stop();
                    break;
            }
        }
        
        void PlayTick(AudioClip clip)
        {
            audioTickSource.clip = clip;
            audioTickSource.loop = true;
            audioTickSource.Play();
        }
        
        protected override void OnAlert()
        {
            PlayTick(tickClip2);
        }
        
    }
}
