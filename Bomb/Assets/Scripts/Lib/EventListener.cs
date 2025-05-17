using System;
using System.Collections.Generic;

namespace Lib
{
    public class EventListener
    {
        private Lib.EventManager _eventManager = null;
        private readonly Dictionary<string, List<Delegate>> _mapListeners = new();

        public EventListener(Lib.EventManager eventManager)
        {
            _eventManager = eventManager;
        }

        ~EventListener()
        {
            RemoveAllListeners();
            _eventManager = null;
        }

        public void Add(string eventName, Delegate eventHandler)
        {
            _eventManager.Add(eventName, eventHandler);

            if (!_mapListeners.ContainsKey(eventName))
            {
                _mapListeners[eventName] = new List<Delegate>();
            }

            List<Delegate> callbacks = _mapListeners[eventName];
            if (!callbacks.Contains(eventHandler))
            {
                callbacks.Add(eventHandler);
            }
        }

        public void Remove(string eventName, Delegate eventHandler)
        {
            if (_mapListeners.TryGetValue(eventName, out List<Delegate> callbacks))
            {
                if (callbacks.Contains(eventHandler))
                {
                    callbacks.Remove(eventHandler);
                    if (callbacks.Count == 0)
                    {
                        _mapListeners.Remove(eventName);
                    }
                }
            }
        }

        public void RemoveAllListeners()
        {
            foreach (var mapListener in _mapListeners)
            {
                foreach (var @delegate in mapListener.Value)
                {
                    _eventManager.Remove(mapListener.Key, @delegate);
                }
            }

            _mapListeners.Clear();
        }
    }
}