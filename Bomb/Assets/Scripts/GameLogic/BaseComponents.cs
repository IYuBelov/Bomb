using System;
using UnityEngine;


public class GameObserverMonoBehaviour : MonoBehaviour
{
    protected Game gameComponent;
    private Lib.EventManager _eventManager = null;
    
    private Action<GameState> _onGameStateChangedHandler;
    private Action _onCurrentPlayerChangedHandler;
    
    void OnEnable()
    {
        var globalContext = FindFirstObjectByType<GlobalContext>();
        _eventManager = globalContext.GetComponent<GlobalContext>().EventManager;
        if (_eventManager != null)
        {
            _onGameStateChangedHandler = new Action<GameState>(OnStateChanged);
            _onCurrentPlayerChangedHandler = new Action(onCurrentPlayerChanged);
            _eventManager.Add(Events.evGameStateChanged, _onGameStateChangedHandler);
            _eventManager.Add(Events.evCurrentPlayerChanged, _onCurrentPlayerChangedHandler);
        }
    }
    
    void OnDisable()
    {
        if (_eventManager != null)
        {
            _eventManager.Remove(Events.evGameStateChanged, _onGameStateChangedHandler);
            _eventManager.Remove(Events.evCurrentPlayerChanged, _onCurrentPlayerChangedHandler);
        }
    }
    
    protected virtual void Start()
    {
        GameObject gameObject = GameObject.FindWithTag("Game");
        gameComponent = gameObject.GetComponent<Game>();
        UpdateState(gameComponent.State);
    }
    
    void OnStateChanged(GameState state)
    {
        UpdateState(state);
    }   
    
    void onCurrentPlayerChanged()
    {
        UpdateCurrentPlayer();
    }

    protected virtual void UpdateState(GameState state)
    {

    }

    protected virtual void UpdateCurrentPlayer()
    {

    }
}