using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerName : GameObserverMonoBehaviour
{
    Text textComponent;
    // Start is called before the first frame update
    protected override void Start()
    {
        textComponent = GetComponent<Text>();
        base.Start();
    }

    protected override void UpdateCurrentPlayer()
    {
        UpdateState(gameComponent.State);
    }

    protected override void UpdateState(GameState state)
    {
        if (state == GameState.PLAY || state == GameState.COUNTDOWN || state == GameState.EXPLOSION || state == GameState.READY_TO_START)
        {
            textComponent.text = gameComponent.getCurrentPlayer().Name;
        }
        else
        {
            textComponent.text = "";
        }
    }
}
