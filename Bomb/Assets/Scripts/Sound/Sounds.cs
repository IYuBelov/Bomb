using Common;
using GameLogic;
using UnityEngine;

//using Contribute;

namespace Sound
{
    public class Sounds : GameObserverMonoBehaviour
    {
        float alertTimer = 0;
        bool isAlert = false;

        protected override void Start()
        {
            base.Start();
            alertTimer = 0;
            isAlert = false;
        }

        protected override void OnAlert()
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
        protected override void OnStateChanged(GameState state)
        {
            if (state == GameState.Play)
            {
                alertTimer = 0;
                //AudioManager.Instance.PlayEffect(SoundID.PLAY);
                //AudioManager.Instance.PlayMusic(SoundID.Background);
            }
            else if (state == GameState.Explosion)
            {
                //AudioManager.Instance.PlayEffect(SoundID.EXPLOSION);
            }
            else if (state == GameState.Result)
            {
                //AudioManager.Instance.PlayEffect(SoundID.RESULT);
            }
            else
            {
                //AudioManager.Instance.StopMusic();
            }
        }
    }
}