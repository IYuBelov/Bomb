using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void Start()
    {
        base.Start();
        gameComponent.evAlert += OnAlert;
    }

    void OnAlert()
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
    protected override void UpdateState(GameState state)
    {
        if (state == GameState.PLAY)
        {
            alertTimer = 0;
            PlayClip(playSource, playStateSound);
            backgroundTicksSource.loop = true;
            backgroundTicksSource.Play();
        }
        else if (state == GameState.EXPLOSION)
        {
            alertSource.Stop();
            explosionSource.Play();
            backgroundTicksSource.Stop();

        }
        else if (state == GameState.RESULT)
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
