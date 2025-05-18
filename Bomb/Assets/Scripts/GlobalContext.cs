using Lib;
using Common;
using Unity.VisualScripting;
using UnityEngine;
using Event = Lib.Event;

public class GlobalContext : MonoBehaviour
{
    private EventManager _eventManager;
    
    void Awake()
    {
        _eventManager = new ();
        this.AddComponent<Debugger>();
    }

    public EventListener MakeEventListener()
    {
        return new EventListener(this._eventManager);
    }
    
    public Event MakeEvent()
    {
        return new Event(this._eventManager);
    }
}
