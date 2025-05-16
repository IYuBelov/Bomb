using System;
using System.Collections.Generic;

namespace Lib
{
    public class EventManager
    {
        private Dictionary<string, Delegate> _eventDictionary = new();

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

        public void Call(string eventType, params object[] args)
        {
            if (_eventDictionary.ContainsKey(eventType))
            {
                _eventDictionary[eventType].DynamicInvoke(args);
            }
        }

        public void Clear()
        {
            _eventDictionary.Clear();
        }
    }
}