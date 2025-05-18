using System;
using Common;
using UnityEngine;

namespace Sound
{
    public class GameSound : GameObserverMonoBehaviour
    {

        [SerializeField]
        AudioSource audioSource;

        [SerializeField]
        AudioClip countdownClip;

        protected override void Start()
        {
            _eventListener.Add(Events.EvCountDownTickChanged, new Action<int>(OnCountDownTickChanged));
        }

        void OnCountDownTickChanged(int count)
        {
            audioSource.PlayOneShot(countdownClip);
        }
    }
}
