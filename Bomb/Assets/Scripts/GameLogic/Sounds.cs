using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Contribute;

public class Sounds : GameObserverMonoBehaviour
{
    float alertTimer = 0;
    bool isAlert = false;

    protected override void Start()
    {
        base.Start();
        alertTimer = 0;
        isAlert = false;
        gameComponent.evAlert += OnAlert;
    }

    void OnAlert()
    {
        isAlert = true;
        alertTimer = 5; 
        //AudioManager.Instance.PlayEffect(SoundID.ALERT);
        //AudioManager.Instance.StopMusic();
    }

    void Update()
    {
        if (isAlert)
        {
            alertTimer -= Time.deltaTime;
            if (alertTimer <= 0)
            {
                //AudioManager.Instance.PlayEffect(SoundID.ALERT);
                alertTimer = 0;
                isAlert = false;
            }
        }
    }

    // Update is called once per frame
    protected override void UpdateState(GameState state)
    {
        if (state == GameState.PLAY)
        {
            alertTimer = 0;
            //AudioManager.Instance.PlayEffect(SoundID.PLAY);
            //AudioManager.Instance.PlayMusic(SoundID.Background);
        }
        else if (state == GameState.EXPLOSION)
        {
            //AudioManager.Instance.PlayEffect(SoundID.EXPLOSION);

        }
        else if (state == GameState.RESULT)
        {
            //AudioManager.Instance.PlayEffect(SoundID.RESULT);
        }
        else
        {
            //AudioManager.Instance.StopMusic();
        }
    }
}
