using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;

public class Debugger : MonoBehaviour
{
    private EventManager _eventManager = null;
    private Action<GameState> _onGameStateChangedHandler;
    
    
    void OnEnable()
    {
        _eventManager = FindFirstObjectByType<EventManager>();
        if (_eventManager != null)
        {
            _onGameStateChangedHandler = new Action<GameState>(OnStateChanged);
            _eventManager.Add(Events.evGameStateChanged, _onGameStateChangedHandler);
        }
    }
    
    void OnDisable()
    {
        if (_eventManager != null && _onGameStateChangedHandler != null)
        {
            _eventManager.Remove(Events.evGameStateChanged, _onGameStateChangedHandler);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("<><><> Debugger: Start");
        GameObject gameObject = GameObject.FindWithTag("Game");
        Game gameComponent = gameObject.GetComponent<Game>();
        //gameComponent.evStateChanged += this.onStateChanged;
        gameComponent.evAlert += this.onAlert;
        gameComponent.evCurrentPlayerChanged += this.onCurrentPlayerChanged;
    }

    void OnStateChanged(GameState state)
    {
        Debug.Log("<><><> Debugger onStateChanged " + state.ToString());
    }

    void onAlert()
    {
        Debug.Log("<><><> Debugger onAlert");
    }
    void onCurrentPlayerChanged()
    {
        Debug.Log("<><><> Debugger onCurrentPlayerChanged");
    }

}
