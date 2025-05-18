using System;
using GameLogic;
using UnityEngine;

namespace Common
{
    public class GameObserverMonoBehaviour : MonoBehaviour
    {
        
        void OnDestroy()
        {
            _eventListener = null;
            GameComponent = null;
        }

        protected Game GameComponent;
        private Lib.EventListener _eventListener;

        void OnEnable()
        {
            var globalContext = FindFirstObjectByType<GlobalContext>();
            _eventListener = globalContext.MakeEventListener();
            _eventListener.Add(Events.EvGameStateChanged, new Action<GameState>(OnStateChanged));
            _eventListener.Add(Events.EvCurrentPlayerChanged, new Action(OnCurrentPlayerChanged));
            _eventListener.Add(Events.EvAlert, new Action(OnAlert));
        }

        void OnDisable()
        {
            _eventListener.RemoveAllListeners();
        }

        protected virtual void Start()
        {
            var game = GameObject.Find("Game");
            GameComponent = game.GetComponent<Game>();
        }

        protected virtual void OnStateChanged(GameState state)
        {
            
        }

        protected virtual void OnCurrentPlayerChanged()
        {

        }

        protected virtual void OnAlert()
        {

        }
    }
}