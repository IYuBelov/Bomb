using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Подключаем TextMeshPro

namespace Lib.Unity.UI
{
    public class TGPlayerSelectionWidget : MonoBehaviour
    {
        [Header("UI Elements")]
        public Button openModalButton;  // Кнопка для открытия модального окна
        public GameObject modalPanel;    // Панель модального окна
        public TMP_InputField playerNameInputField; // Поле ввода имени игрока (TextMeshPro)
        public Button addPlayerButton;   // Кнопка добавления игрока
        public GameObject playerIconPrefab; // Префаб иконки игрока (TextMeshPro - TMP_Text)
        public Transform playerCircleContainer; // Контейнер для иконок (где они будут расположены по кругу)
        public Button closeButton; // Кнопка закрытия модального окна

        [Header("Settings")]
        public float circleRadius = 150f; // Радиус круга расположения игроков
        public float iconScale = 1.0f;    // Масштаб иконок

        private List<GameObject> playerIcons = new List<GameObject>(); // Список иконок игроков
        protected List<string> PlayerNames = new List<string>(); // Список имен игроков
        protected List<Color> PlayerColors = new ();

        private GameObject currentlyDraggedIcon = null; // Ссылка на перетаскиваемую иконку

        public static string 
            evAddPlayer = "SelectionWidget.AddPlayer",
            evRemovePlayer = "SelectionWidget.evRemovePlayer";

        protected virtual void Start()
        {
            // Инициализация и подключение событий
            //openModalButton.onClick.AddListener(OpenModal);
            addPlayerButton.onClick.AddListener(AddPlayer);
            //closeButton.onClick.AddListener(CloseModal);
            modalPanel.SetActive(false);
            circleRadius = (float)(gameObject.GetComponent<RectTransform>().rect.width * 0.35);
        }

        protected virtual void SendEvent(string eventName, params object[] args)
        {
            
        }

        void OpenModal()
        {
            modalPanel.SetActive(true);
            playerNameInputField.text = ""; // Очищаем поле ввода
        }

        void CloseModal()
        {
            modalPanel.SetActive(false);
        }

        void AddPlayer()
        {
            string playerName = playerNameInputField.text.Trim();

            if (string.IsNullOrEmpty(playerName))
            {
                Debug.LogWarning("Имя игрока не может быть пустым.");
                return;
            }

            if (PlayerNames.Contains(playerName))
            {
                Debug.LogWarning("Игрок с таким именем уже существует.");
                return;
            }

            PlayerNames.Add(playerName);
            CreatePlayerIcon(playerName);

            playerNameInputField.text = ""; // Очищаем поле ввода после добавления
            UpdatePlayerPositions();
        }

        public void CreatePlayerIcon(string playerName)
        {
            GameObject newIcon = Instantiate(playerIconPrefab, this.transform);
            if (PlayerColors.Count > 0)
            {
                var image = newIcon.GetComponent<Image>();
                image.color = PlayerColors[PlayerNames.Count];
            }
            
            TMP_Text iconText = newIcon.GetComponentInChildren<TMP_Text>(); // Получаем TMP_Text
            if (iconText != null) {
                iconText.text = playerName;
            } else {
                Debug.LogError("Не найден компонент TMP_Text в префабе иконки игрока!");
            }
            newIcon.transform.localScale = Vector3.one * iconScale; // Устанавливаем масштаб
            newIcon.AddComponent<PlayerIconDragHandler>(); // Добавляем скрипт для перетаскивания

            PlayerIconDragHandler dragHandler = newIcon.GetComponent<PlayerIconDragHandler>();
            dragHandler.playerSelectionWidget = this; // Передаем ссылку на виджет

            playerIcons.Add(newIcon);
            SendEvent("");
        }

        protected void UpdatePlayerPositions()
        {
            // Расположение иконок по кругу
            float angleStep = 360f / playerIcons.Count;
            for (int i = 0; i < playerIcons.Count; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                var rect = gameObject.GetComponent<RectTransform>().rect;
                float coeff = 0.35f;
                Vector3 pos = new Vector3(Mathf.Cos(angle) * rect.width * coeff, Mathf.Sin(angle) * rect.height * coeff, 0);
                playerIcons[i].transform.localPosition = pos;
            }
        }

        public void StartDragging(GameObject icon)
        {
            currentlyDraggedIcon = icon;
        }

        public void StopDragging(GameObject icon)
        {
            currentlyDraggedIcon = null;
            UpdatePlayerPositions(); // Пересчитываем позиции после перетаскивания
        }

        public void SwapPlayerPositions(GameObject icon1, GameObject icon2)
        {
            // Меняем местами позиции иконок в списке
            int index1 = playerIcons.IndexOf(icon1);
            int index2 = playerIcons.IndexOf(icon2);

            if (index1 != -1 && index2 != -1)
            {
                GameObject temp = playerIcons[index1];
                playerIcons[index1] = playerIcons[index2];
                playerIcons[index2] = temp;

                UpdatePlayerPositions(); // Обновляем позиции на экране
            }
            else
            {
                Debug.LogError("Одна из иконок не найдена в списке!");
            }
        }


        public void RemovePlayer(GameObject icon)
        {
            int index = playerIcons.IndexOf(icon);

            if (index != -1)
            {
                string playerName = icon.GetComponentInChildren<TMP_Text>().text; // Получаем имя из иконки
                PlayerNames.Remove(playerName);  // Удаляем имя из списка имен

                playerIcons.RemoveAt(index);
                Destroy(icon);
                UpdatePlayerPositions();
            }
        }

        // Скрипт для обработки перетаскивания иконки (отдельный класс)
        public class PlayerIconDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
        {
            public TGPlayerSelectionWidget playerSelectionWidget; // Ссылка на основной виджет
            private Vector3 startPosition;

            public void OnBeginDrag(PointerEventData eventData)
            {
                startPosition = transform.localPosition;
                playerSelectionWidget.StartDragging(gameObject);
                //GetComponent<CanvasGroup>().blocksRaycasts = false; // Позволяет перетаскивать над другими иконками
            }

            public void OnDrag(PointerEventData eventData)
            {
                transform.position = eventData.position; // Перемещаем иконку под курсор
            }

            public void OnEndDrag(PointerEventData eventData)
            {
                playerSelectionWidget.StopDragging(gameObject);
                //GetComponent<CanvasGroup>().blocksRaycasts = true;  // Возвращаем блокировку лучей
                // Проверка на пересечение с другими иконками
                foreach (GameObject icon in playerSelectionWidget.playerIcons)
                {
                    if (icon != gameObject)
                    {
                        if (RectTransformExtensions.Intersect(gameObject, icon))
                        {
                            playerSelectionWidget.SwapPlayerPositions(gameObject, icon);
                            return;
                        }
                    }
                }

                transform.localPosition = startPosition; // Возвращаем на начальную позицию, если не было пересечения
            }
        }

        // Функция расширения для проверки пересечения RectTransform (необязательно, если у вас есть своя реализация)
        public static class RectTransformExtensions
        {
            public static bool Intersect(GameObject icon1, GameObject icon2)
            {
                RectTransform rect1 = icon1.GetComponent<RectTransform>();
                RectTransform rect2 = icon2.GetComponent<RectTransform>();

                Rect r1 = new Rect(icon1.transform.localPosition.x, icon2.transform.localPosition.y,
                    rect1.rect.width, rect1.rect.height);
                Rect r2 = new Rect(icon2.transform.localPosition.x, icon2.transform.localPosition.y,
                    rect2.rect.width, rect2.rect.height);

                return r1.Overlaps(r2);
            }

            private static Rect GetRect(Vector3[] corners)
            {
                return new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
            }
        }
    }
}