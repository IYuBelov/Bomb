using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class StartButton : MonoBehaviour
    {
        public Sprite activeSprite;
        public Sprite inactiveSprite;
        private GlobalContext _globalContext;
        private Lib.EventListener _eventListener;
        
        private void OnEnable()
        {
            _globalContext = FindFirstObjectByType<GlobalContext>();
            _eventListener = _globalContext.MakeEventListener();
        }

        private void UpdateState()
        {
            var isActive = _globalContext.PData().playerNames.Count >= 2;
            var button = GetComponent<Button>();
            button.interactable = isActive;
            var image = GetComponent<Image>();
            image.sprite = isActive ? activeSprite : inactiveSprite;
        }
        
        void Start()
        {
            UpdateState();
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnStartGame);
        }

        void OnStartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
