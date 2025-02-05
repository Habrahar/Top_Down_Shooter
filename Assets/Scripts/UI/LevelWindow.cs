    using System.Collections.Generic;
    using Level;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    namespace UI
    {


        public class LevelWindow : WindowBase
        {
            [SerializeField] private GameObject levelButtonPrefab; // Префаб кнопки уровня
            [SerializeField] private Transform levelsContainer; // Контейнер для уровней (Scroll View Content)
            [SerializeField] private Button closeButton; // Кнопка закрытия окна

            private LevelManager levelManager; // Ссылка на LevelManager
            [SerializeField] private GameManager gm;
            private List<Button> levelButtons = new List<Button>(); // Список кнопок уровней

            private void Awake()
            {
                // Находим LevelManager
                levelManager = FindObjectOfType<LevelManager>();
                if (levelManager == null)
                {
                    Debug.LogError("LevelManager не найден!");
                    return;
                }

                // Закрываем окно при нажатии на кнопку закрытия
                closeButton.onClick.AddListener(CloseWindow);
            }
            protected override void OnClose()
            {
                base.OnClose();
            }
            protected override void OnOpen()
            {
                base.OnClose();
            }
            public override void UpdateWindow()
            {
                
            }

            private void OnEnable()
            {
                // При открытии окна обновляем список уровней
                UpdateLevels();
            }

            // Обновление списка уровней
            private void UpdateLevels()
            {
                // Очищаем старые кнопки
                foreach (Button button in levelButtons)
                {
                    Destroy(button.gameObject);
                }

                levelButtons.Clear();

                // Получаем список уровней из LevelManager
                List<LevelData> levels = levelManager.GetLevels();
                int currentLevel = gm.currentLevel;

                // Создаем кнопки для каждого уровня
                for (int i = 0; i < levels.Count; i++)
                {
                    GameObject buttonObject = Instantiate(levelButtonPrefab, levelsContainer);
                    Button levelButton = buttonObject.GetComponent<Button>();
                    TextMeshProUGUI levelText = buttonObject.transform.Find("LevelText").GetComponentInChildren<TextMeshProUGUI>();
                    Slider sliderPorgres = buttonObject.transform.Find("Slider").GetComponentInChildren<Slider>();
                    Image lockIcon = buttonObject.transform.Find("LockIcon").GetComponent<Image>();

                    // Устанавливаем номер уровня
                    levelText.text = (i + 1).ToString();

                    // Блокируем уровни, которые больше текущего
                    if (i + 1 > currentLevel)
                    {
                        levelButton.interactable = false;
                        sliderPorgres.value = 0;
                        lockIcon.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log($"Уровень {i + 1}: Прогресс = {levelManager.GetLevelProgress(i)}");
                        sliderPorgres.value = levelManager.GetLevelProgress(i);
                        levelButton.interactable = true;
                        lockIcon.gameObject.SetActive(false);
                    }

                    // Добавляем обработчик нажатия на кнопку
                    int levelIndex = i; // Локальная переменная для замыкания
                    levelButton.onClick.AddListener(() => OnLevelButtonClicked(levelIndex));

                    // Сохраняем кнопку в список
                    levelButtons.Add(levelButton);
                }
            }

            // Обработчик нажатия на кнопку уровня
            private void OnLevelButtonClicked(int levelIndex)
            {
                Debug.Log($"Выбран уровень: {levelIndex + 1}");
                levelManager.StartNextLevel(levelIndex ); // Загружаем уровень
                CloseWindow(); // Закрываем окно
            }

            // Закрытие окна
            private void CloseWindow()
            {
                gameObject.SetActive(false);
            }
        }
    }