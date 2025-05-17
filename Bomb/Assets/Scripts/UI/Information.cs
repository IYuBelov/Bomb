using Common;
using GameLogic;
using UnityEngine.UI;

namespace UI
{
    public class Information : GameObserverMonoBehaviour
    {
        Text textComponent;

        // Start is called before the first frame update
        protected override void Start()
        {
            textComponent = GetComponent<Text>();
            base.Start();
        }

        protected override void OnStateChanged(GameState state)
        {
            if (state == GameState.Play)
            {
                var card = GameComponent.CurrentCard;
                string place;
                if (card.Condition == WordCondition.Begin)
                {
                    place = "� ������ �����";
                }
                else if (card.Condition == WordCondition.Anywhere)
                {
                    place = "� ����� �����";
                }
                else
                {
                    place = "� ����� �����";
                }

                textComponent.text = string.Format("<size=60>({1})</size>\n<size=90>{0}</size>", card.Word, place);
            }
            else if (state == GameState.Explosion)
            {
                var player = GameComponent.getCurrentPlayer();
                textComponent.text = string.Format("<size=75>����: {0}</size>\n<size=39>���� ���� �������!</size>", player.Score);
            }
            else if (state == GameState.ReadyToStart)
            {
                var player = GameComponent.getCurrentPlayer();
                textComponent.text = "������ ����� ������ ����� �����!";
            }
            else
            {
                textComponent.text = "";
            }
        }
    }
}
