using Account;
using Lib;
using UnityEngine;
using Event = Lib.Event;

public class GlobalContext : MonoBehaviour
{
    private EventManager _eventManager;
    public AccountPersistentObject pData;
    
    void Awake()
    {
        _eventManager = new EventManager();
        pData = GetComponent<AccountPersistentObject>();
    }

    public EventListener MakeEventListener()
    {
        return new EventListener(this._eventManager);
    }
    
    public Event MakeEvent()
    {
        return new Event(this._eventManager);
    }   
    
    public AccountPersistentData PData()
    {
        return pData.data;
    }
}

