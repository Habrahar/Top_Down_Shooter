using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI;

public class GoldWindowManager : WindowBase
{
    [SerializeField] private Button _watchAdButton; // Кнопка для просмотра рекламы
    [SerializeField] private TextMeshProUGUI _timerText; // Текст для отображения таймера
    [SerializeField] public int _rewardAmount = 200; // Количество золота за просмотр
    [SerializeField] private TextMeshProUGUI _rewardAmountText; // Текст для отображения количества награды
    [SerializeField] private int _cooldownMinutes = 1; // Время ожидания в минутах

    [SerializeField] protected int countWatched = 1; //НУжно сохранить параметр
    private DateTime _lastAdWatchTime; // Время последнего просмотра рекламы
    private Coroutine _updateCoroutine; // Корутина для обновления таймера

    protected override void OnOpen()
    {
        base.OnOpen();
        _rewardAmountText.text = (_rewardAmount).ToString(); // Устанавливаем текст награды
        LoadLastAdWatchTime(); // Загружаем время последнего просмотра
        StartUpdateCoroutine(); // Запускаем корутину для обновления таймера
    }

    protected override void OnClose()
    {
        base.OnClose();
        StopUpdateCoroutine(); // Останавливаем корутину при закрытии окна
    }

    public override void UpdateWindow()
    {
        throw new NotImplementedException();
    }

    private void LoadLastAdWatchTime()
    {
        // Загружаем время последнего просмотра из PlayerPrefs
        string lastAdWatchTimeString = PlayerPrefs.GetString("LastAdWatchTime", string.Empty);

        if (!string.IsNullOrEmpty(lastAdWatchTimeString))
        {
            _lastAdWatchTime = DateTime.Parse(lastAdWatchTimeString);
        }
        else
        {
            _lastAdWatchTime = DateTime.MinValue; // Если время не сохранено, используем минимальное значение
        }
    }

    private void SaveLastAdWatchTime()
    {
        // Сохраняем текущее время в PlayerPrefs
        PlayerPrefs.SetString("LastAdWatchTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    public void OnWatchAdButtonClicked()
    {
        // Вызывается при нажатии на кнопку просмотра рекламы
        if (DateTime.Now - _lastAdWatchTime >= TimeSpan.FromMinutes(_cooldownMinutes))
        {
            StopUpdateCoroutine();
            // Сохраняем время последнего просмотра
            countWatched++;
            _cooldownMinutes *= countWatched;
            SaveLastAdWatchTime();
            
            _rewardAmount *= countWatched; 
            _rewardAmountText.text = (_rewardAmount).ToString(); // Устанавливаем текст награды
            LoadLastAdWatchTime(); // Загружаем время последнего просмотра
            StartUpdateCoroutine(); // Запускаем корутину для обновления таймера
        }
    }

    private void UpdateButtonState()
    {
        if (_watchAdButton == null || _timerText == null)
        {
            Debug.LogWarning("Кнопка или текст таймера не назначены!");
            return;
        }

        TimeSpan timeSinceLastAd = DateTime.Now - _lastAdWatchTime;

        if (timeSinceLastAd.TotalMinutes >= _cooldownMinutes)
        {
            // Если время ожидания прошло, кнопка активна
            _watchAdButton.interactable = true;
            _timerText.text = "Смотреть рекламу";
            Debug.Log("Кнопка активна.");
        }
        else
        {
            // Если время ожидания не прошло, кнопка неактивна
            _watchAdButton.interactable = false;

            // Вычисляем оставшееся время
            TimeSpan remainingTime = TimeSpan.FromMinutes(_cooldownMinutes) - timeSinceLastAd;
            _timerText.text = $"Доступно через: {remainingTime:mm\\:ss}";
            Debug.Log("Кнопка неактивна.");
        }
    }

    private void StartUpdateCoroutine()
    {
        // Запускаем корутину, если она еще не запущена
        if (_updateCoroutine == null)
        {
            _updateCoroutine = StartCoroutine(UpdateTimerCoroutine());
        }
    }

    private void StopUpdateCoroutine()
    {
        // Останавливаем корутину, если она запущена
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);
            _updateCoroutine = null;
        }
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (true)
        {
            // Обновляем состояние кнопки и таймера
            UpdateButtonState();

            // Ждем 1 секунду перед следующим обновлением
            yield return new WaitForSeconds(1f);
        }
    }
}