using System.Collections;
using System.Collections.Generic;
using Common;
using GameLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputProcessor : GameObserverMonoBehaviour
{
    // [SerializeField]
    // AudioSource playaudio;
    //
    // [SerializeField]
    // GameObject bombPrefab;

    GameObject _explosion ;
    private Lib.Event _event;
    private Vector2 _position;

    protected override void Start()
    {
        base.Start();
        var globalContext = FindFirstObjectByType<GlobalContext>();
        _event = globalContext.MakeEvent();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMouse();
        ProcessTouch();
    }

    private void ProcessMouse()
    {
#if UNITY_STANDALONE_WIN
        if (GameComponent.State == GameState.Play)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameComponent.nextPlayer();
                _event.Call(Events.EvTouchNextPlayer);
            }

            if (Input.GetMouseButtonUp(1))
            {
                GameComponent.prevPlayer();
                _event.Call(Events.EvTouchPrevPlayer);
            }
        }
        else if (GameComponent.State == GameState.ReadyToStart)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameComponent.startRound();
                _event.Call(Events.EvTouchStartRound);
            }
        }
        else if (GameComponent.State == GameState.Result)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameComponent.startRound();
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }
#endif
    }

    private void ProcessTouch()
    {
        if (GameComponent.State == GameState.Play)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    if ((_position - touch.position).magnitude > 650)
                    {
                        GameComponent.prevPlayer();
                        _event.Call(Events.EvTouchPrevPlayer);
                    }
                    else
                    {
                        GameComponent.nextPlayer();
                        _event.Call(Events.EvTouchNextPlayer);
                    }
                }

                if (touch.phase == TouchPhase.Began)
                {
                    _position = touch.position;
                }
            }
        }
        else if (GameComponent.State == GameState.ReadyToStart)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                GameComponent.startRound();
                _event.Call(Events.EvTouchStartRound);
            }
        }
        else if (GameComponent.State == GameState.Result)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                GameComponent.startRound();
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }
    }

    protected override void OnStateChanged(GameState state)
    {
        if (GameComponent.State == GameState.Explosion)
        {
            //_explosion = (GameObject)Instantiate(bombPrefab, new Vector3(0, -2.8f, 0), Quaternion.identity);
        }
        else if (_explosion != null)
        {
            // Destroy(_explosion);
            // _explosion = null;
        }
    }
}