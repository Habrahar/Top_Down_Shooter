using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldWindowManager : WindowBase
{
    [SerializeField] private Button _watchAdButton; // Кнопка для просмотра рекламы
    [SerializeField] private TextMeshProUGUI _timerText; // Текст для отображения таймера
    [SerializeField] public int _rewardAmount = 100; // Количество золота за просмотр
    [SerializeField] private TextMeshProUGUI _rewardAmounText; // Текст для отображения таймера
    [SerializeField] private int _cooldownMinutes = 60; // Время ожидания в минутах

    private DateTime _lastAdWatchTime; // Время последнего просмотра рекламы
    
    protected override void OnOpen()
    {
        base.OnOpen();
        _rewardAmounText.text = _rewardAmount.ToString();
        LoadLastAdWatchTime(); // Загружаем время последнего просмотра
        UpdateButtonState(); // Обновляем состояние кнопки
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

    private void UpdateButtonState()
    {
        TimeSpan timeSinceLastAd = DateTime.Now - _lastAdWatchTime;

        if (timeSinceLastAd.TotalMinutes >= _cooldownMinutes)
        {
            // Если время ожидания прошло, кнопка активна
            _watchAdButton.interactable = true;
            _timerText.text = "Смотреть рекламу";
        }
        else
        {
            // Если время ожидания не прошло, кнопка неактивна
            _watchAdButton.interactable = false;

            // Вычисляем оставшееся время
            TimeSpan remainingTime = TimeSpan.FromMinutes(_cooldownMinutes) - timeSinceLastAd;
            _timerText.text = $"Доступно через: {remainingTime:mm\\:ss}";
        }
    }

    public void OnWatchAdButtonClicked()
    {
        // Вызывается при нажатии на кнопку просмотра рекламы
        if (DateTime.Now - _lastAdWatchTime >= TimeSpan.FromMinutes(_cooldownMinutes))
        {
            // Сохраняем время последнего просмотра
            SaveLastAdWatchTime();

            // Обновляем состояние кнопки
            UpdateButtonState();
        }
    }


    public override void UpdateWindow()
    {
        // Обновляем состояние кнопки каждый кадр (или через определенные интервалы)
        UpdateButtonState();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}