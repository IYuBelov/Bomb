using Lib;
using Common;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using Event = Lib.Event;

public class GlobalContext : MonoBehaviour
{
    public EventManager EventManager;
    
    void Awake()
    {
        EventManager = new ();
        this.AddComponent<Debugger>();
    }

    public EventListener MakeEventListener()
    {
        return new EventListener(this.EventManager);
    }
    
    public Event MakeEvent()
    {
        return new Event(this.EventManager);
    }
}
