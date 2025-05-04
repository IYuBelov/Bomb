using UnityEngine;
using UnityEngine.UI;


public class GameObserverMonoBehaviour : MonoBehaviour
{
    protected Game gameComponent;
    protected virtual void Start()
    {
        GameObject gameObject = GameObject.FindWithTag("Game");
        gameComponent = gameObject.GetComponent<Game>();
        gameComponent.evStateChanged += this.OnStateChanged;
        gameComponent.evCurrentPlayerChanged += this.onCurrentPlayerChanged;

        UpdateState(gameComponent.State);
    }
    void OnStateChanged(GameState state)
    {
        UpdateState(state);
    }   
    
    void onCurrentPlayerChanged()
    {
        UpdateCurrentPlayer();
    }

    protected virtual void UpdateState(GameState state)
    {

    }

    protected virtual void UpdateCurrentPlayer()
    {

    }

    void OnRemoveComponent()
    {
        print("DDDD");
    }
}