using Common;
using GameLogic;
using UnityEngine.UI;

namespace UI
{
    public class PlayerName : GameObserverMonoBehaviour
    {
        Text textComponent;
        // Start is called before the first frame update
        protected override void Start()
        {
            textComponent = GetComponent<Text>();
            base.Start();
        }

        protected override void OnCurrentPlayerChanged()
        {
            UpdateState(GameComponent.State);
        }

        protected override void OnStateChanged(GameState state)
        {
            UpdateState(state);
        }

        private void UpdateState(GameState state)
        {
            if (state == GameState.Play || state == GameState.Countdown || state == GameState.Explosion || state == GameState.ReadyToStart)
            {
                textComponent.text = GameComponent.getCurrentPlayer().Name;
            }
            else
            {
                textComponent.text = "";
            }
        }
    }
}
