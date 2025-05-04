using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputProcessor : GameObserverMonoBehaviour
{
    [SerializeField]
    AudioSource playaudio;

    [SerializeField]
    GameObject bombPrefab;

    GameObject explosion = null;

    private Vector2 position;

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (gameComponent.State == GameState.PLAY)
        {
            if (Input.GetMouseButtonUp(0))
            {
                gameComponent.nextPlayer();
                playaudio.Play();
            }

            if (Input.GetMouseButtonUp(1))
            {
                gameComponent.prevPlayer();
                playaudio.Play();
            }
        }
        else if (gameComponent.State == GameState.READY_TO_START)
        {
            if (Input.GetMouseButtonUp(0))
            {
                gameComponent.startRound();
                playaudio.Play();
            }
        }
        else if (gameComponent.State == GameState.RESULT)
        {
            if (Input.GetMouseButtonUp(0))
            {
                gameComponent.startRound();
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }
#endif


        if (gameComponent.State == GameState.PLAY)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    if ((position - touch.position).magnitude > 650)
                    {
                        gameComponent.prevPlayer();
                        playaudio.Play();
                    }
                    else
                    {
                        gameComponent.nextPlayer();
                        playaudio.Play();
                    }

                }

                if (touch.phase == TouchPhase.Began)
                {
                    position = touch.position;

                }
            }
        }
        else if (gameComponent.State == GameState.READY_TO_START)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                gameComponent.startRound();
                playaudio.Play();

            }
        }
        else if (gameComponent.State == GameState.RESULT)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                gameComponent.startRound();
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

            }
        }
    }

    protected override void UpdateState(GameState state)
    {
        if (gameComponent.State == GameState.EXPLOSION)
        {
            explosion = (GameObject)Instantiate(bombPrefab, new Vector3(0, -2.8f, 0), Quaternion.identity);
        }
        else if (explosion != null)
        {
            Destroy(explosion);
            explosion = null;
        }
    }

}
