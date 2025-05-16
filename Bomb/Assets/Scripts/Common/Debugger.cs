using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;
using EventManager = Lib.EventManager;

namespace Common
{
    public class Debugger : MonoBehaviour
    {
        private EventManager _eventManager = null;
        private Action<GameState> _onGameStateChangedHandler;
        private Action _onCurrentPlayerChangedHandler;
        private Action _onAlertHandler;
        
        void OnEnable()
        {
            var globalContext = FindFirstObjectByType<GlobalContext>();
            _eventManager = globalContext.GetComponent<GlobalContext>().EventManager;
            if (_eventManager != null)
            {
                _onGameStateChangedHandler = new Action<GameState>(OnStateChanged);
                _onCurrentPlayerChangedHandler = new Action (onCurrentPlayerChanged);
                _onAlertHandler = new Action (onAlert);
                _eventManager.Add(Events.evGameStateChanged, _onGameStateChangedHandler);
                _eventManager.Add(Events.evCurrentPlayerChanged, _onCurrentPlayerChangedHandler);
                _eventManager.Add(Events.evAlert, _onAlertHandler);
            }
        }

        void OnDisable()
        {
            if (_eventManager != null)
            {
                _eventManager.Remove(Events.evGameStateChanged, _onGameStateChangedHandler);
                _eventManager.Remove(Events.evCurrentPlayerChanged, _onCurrentPlayerChangedHandler);
                _eventManager.Remove(Events.evAlert, _onAlertHandler);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("<><><> Debugger: Start");
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

}
