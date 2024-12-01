using System;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Settings")]
    public float triggerTime = 5f; // Время, которое игрок должен находиться в области
    public string playerTag = "Player"; // Тег игрока

    private float timeInArea = 0f; // Текущее время в области
    private bool playerInside = false;
    public TurretComponent tc;

    void Update()
    {
        if (playerInside)
        {
            timeInArea += Time.deltaTime;

            if (timeInArea >= triggerTime)
            {
                tc.turretUpdate();
                timeInArea = 0f; // Сброс таймера (или можно отключить область)
            }
        }
        else
        {
            // Сбрасываем таймер, если игрок вышел из области
            timeInArea = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInside = true;
            //работает
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInside = false;
        }
    }
}