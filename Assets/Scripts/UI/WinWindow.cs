using System.Collections;
using New;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class WinWindow : WindowBase
    {
        public GameManager gm;

        [SerializeField] private Button adButton; // Ссылка на кнопку
        [SerializeField] private Button menuButton; // Ссылка на кнопку
        [SerializeField] private float buttonDelay = 3f; // Задержка в секундах

        [Header("Animation Settings")]
        [SerializeField] private float animationDuration = 0.5f; // Длительность анимации

        [Header("Progress Settings")]
        [SerializeField] private Slider progressBar; // Ссылка на Slider для прогресса
        [SerializeField] private TextMeshProUGUI progressText; // Текст для отображения процентов
        [SerializeField] private TextMeshProUGUI reward;
        private int progressValue;

        private RectTransform rectTransform;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            if (menuButton != null)
            {
                menuButton.gameObject.SetActive(false); // Скрываем кнопку при старте
            }

            if (progressBar != null)
            {
                progressBar.value = 0; // Сбрасываем прогресс при старте
            }

            if (progressText != null)
            {
                progressText.text = "0%"; // Сбрасываем текст прогресса
            }
        }

        public override void UpdateWindow()
        {
            // Дополнительная логика обновления окна
        }

        public void ShowAd()
        {
            // Добавить логику показа рекламы
            Debug.Log("Реклама показана");
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            UpdateProgress();
            // Запуск анимации появления окна
            StartCoroutine(AnimateWindowOpen());

            // Запуск таймера для показа кнопки
            if (menuButton != null)
            {
                StartCoroutine(ShowButtonWithDelay());
            }
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        private IEnumerator ShowButtonWithDelay()
        {
            yield return new WaitForSeconds(buttonDelay); // Ожидание
            if (menuButton != null)
            {
                menuButton.gameObject.SetActive(true); // Показываем кнопку
            }
        }

        private IEnumerator AnimateWindowOpen()
        {
            float elapsedTime = 0f;
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0f; // Начальная прозрачность
            canvasGroup.interactable = false; // Отключаем взаимодействие во время анимации
            canvasGroup.blocksRaycasts = false;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / animationDuration;

                // Линейная интерполяция прозрачности
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            canvasGroup.alpha = 1f; // Устанавливаем финальную прозрачность
            canvasGroup.interactable = true; // Включаем взаимодействие
            canvasGroup.blocksRaycasts = true;
        }

        public void UpdateProgress()
        {
            if (gm.spawner.tmp_counter != 0)
            {
                adButton.gameObject.SetActive(true);
                progressValue = gm.spawner.tmp_counter / gm.spawner.counter;
                reward.text = (gm.LevelManager.GetReward() / progressValue).ToString();
            }
            else
            {
                adButton.gameObject.SetActive(false);
                reward.text ="0";
                
            }
            
            
            if (progressBar != null)
            {
                progressBar.value = (progressValue ); // Устанавливаем значение с нормализацией
            }

            if (progressText != null)
            {
                progressText.text = $"{progressValue * 100f}%"; // Устанавливаем текст
            }
        }
    }
}
