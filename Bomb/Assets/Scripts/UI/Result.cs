using Common;
using GameLogic;
using UnityEngine.UI;

namespace UI
{
    public class Result : GameObserverMonoBehaviour
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
            if (state == GameState.Result)
            {
                var result = GameComponent.GetResult();
                string text = string.Format("���������� {0}!\n\n", result[0].Name);
                foreach(Player player in result)
                {
                    text = text + string.Format("{0}:\t\t{1}\n", player.Name, player.Score);
                }

                textComponent.text = text;
            }
            else
            {
                textComponent.text = "";
            }
        }
    }
}
