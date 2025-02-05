using System;
using UnityEngine;
using YG;
public class AdManager : MonoBehaviour
{
    // Синглтон экземпляр
    private static AdManager _instance;
    private int tmp_id;

    // Публичное свойство для доступа к экземпляру
    public static AdManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Ищем существующий экземпляр на сцене
                _instance = FindObjectOfType<AdManager>();

                // Если экземпляр не найден, создаем новый
                if (_instance == null)
                {
                    GameObject adManagerObject = new GameObject("AdManager");
                    _instance = adManagerObject.AddComponent<AdManager>();
                }
            }
            return _instance;
        }
    }

    // Делегат для выдачи награды
    public static event Action OnRewardGoldGranted;
    public static event Action OnRewardGoldAfterlevel;

    // Приватный конструктор, чтобы нельзя было создать экземпляр извне
    private AdManager() { }

    // Метод для запроса рекламы
    public void ShowRewardedAd(int rewardId)
    {
        Debug.Log($"Запрошена реклама с наградой ID: {rewardId}");
        tmp_id = rewardId;
        YandexGame.RewVideoShow(0);
       
    }

    // Callback-метод, который вызывается SDK после завершения рекламы
    private void OnAdCompleted(int rewardId, bool isSuccess)
    {
        if (isSuccess)
        {
            Debug.Log($"Реклама с ID {rewardId} успешно просмотрена. Выдача награды...");
            GiveReward();
            
        }
        else
        {
            Debug.LogWarning($"Реклама с ID {rewardId} не была просмотрена.");
        }
    }
    
    public void GiveReward()
    {
        switch (tmp_id)
        {
            case 1:
                OnRewardGoldGranted?.Invoke(); // Вызываем делегат
                break;
            case 2:
                OnRewardGoldAfterlevel?.Invoke();
                break;
            case 3:
                Debug.Log("Выдана награда: 2 бонусных уровня");
                // Логика выдачи 2 бонусных уровней
                break;
            default:
                Debug.LogWarning($"Неизвестный ID награды:");
                break;
        }
    }
}