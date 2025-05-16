using Lib;
using Common;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GlobalContext : MonoBehaviour
{
    public EventManager EventManager;
    
    void Awake()
    {
        EventManager = new ();
        this.AddComponent<Debugger>();
    }
}
