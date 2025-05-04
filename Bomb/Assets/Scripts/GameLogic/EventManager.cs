using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, Delegate> _eventDictionary = new();

        public void Clear()
        {
            _eventDictionary.Clear();
        }

        public void Add(string eventType, Delegate eventHandler)
        {
            if (!_eventDictionary.ContainsKey(eventType))
            {
                _eventDictionary[eventType] = eventHandler;
            }
            else
            {
                _eventDictionary[eventType] = Delegate.Combine(_eventDictionary[eventType], eventHandler);
            }
        }

        public void Remove(string eventType, Delegate eventHandler)
        {
            if (_eventDictionary.ContainsKey(eventType))
            {
                var currentDelegate = _eventDictionary[eventType];
                currentDelegate = Delegate.Remove(currentDelegate, eventHandler);

                if (currentDelegate == null)
                {
                    _eventDictionary.Remove(eventType);
                }
                else
                {
                    _eventDictionary[eventType] = currentDelegate;
                }
            }
        }

        public void Execute(string eventType, params object[] args)
        {
            if (_eventDictionary.ContainsKey(eventType))
            {
                _eventDictionary[eventType].DynamicInvoke(args);
            }
        }
    }
}