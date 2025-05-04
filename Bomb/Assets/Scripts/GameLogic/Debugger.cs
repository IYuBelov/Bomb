using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("<><><> Debugger: Start");
        GameObject gameObject = GameObject.FindWithTag("Game");
        Game gameComponent = gameObject.GetComponent<Game>();
        gameComponent.evStateChanged += this.onStateChanged;
        gameComponent.evAlert += this.onAlert;
        gameComponent.evCurrentPlayerChanged += this.onCurrentPlayerChanged;
    }

    void onStateChanged(GameState state)
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
