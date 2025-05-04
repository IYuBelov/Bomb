using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Information : GameObserverMonoBehaviour
{
    Text textComponent;

    // Start is called before the first frame update
    protected override void Start()
    {
        textComponent = GetComponent<Text>();
        base.Start();
    }

    protected override void UpdateState(GameState state)
    {
        if (state == GameState.PLAY)
        {
            var card = gameComponent.CurrentCard;
            string place;
            if (card.Condition == WordCondition.BEGINING)
            {
                place = "в начале слова";
            }
            else if (card.Condition == WordCondition.ANYWHERE)
            {
                place = "¬ любом месте";
            }
            else
            {
                place = "в конце слова";
            }

            textComponent.text = string.Format("<size=60>({1})</size>\n<size=90>{0}</size>", card.Word, place);
        }
        else if (state == GameState.EXPLOSION)
        {
            var player = gameComponent.getCurrentPlayer();
            textComponent.text = string.Format("<size=75>очки: {0}</size>\n<size=39>¬аше очко порвано!</size>", player.Score);
        }
        else if (state == GameState.READY_TO_START)
        {
            var player = gameComponent.getCurrentPlayer();
            textComponent.text = "кликни чтобы начать новый раунд!";
        }
        else
        {
            textComponent.text = "";
        }
    }
}
