using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvacuationZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float evacuationTime = 3f; // Время для эвакуации

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI countdownText; // Текст для отображения отсчета

    public delegate void EvacuationComplete();
    public static event EvacuationComplete OnLevelComplete; // Ивент окончания уровня

    private Coroutine evacuationCoroutine;
    private bool isPlayerInZone = false;
    private float currentTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что зашел игрок
        {
            isPlayerInZone = true;
            StartEvacuation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что вышел игрок
        {
            isPlayerInZone = false;
            StopEvacuation();
        }
    }

    private void StartEvacuation()
    {
        if (evacuationCoroutine == null) // Запускаем, только если еще не запущено
        {
            evacuationCoroutine = StartCoroutine(EvacuationCountdown());
        }
    }

    private void StopEvacuation()
    {
        if (evacuationCoroutine != null)
        {
            StopCoroutine(evacuationCoroutine);
            evacuationCoroutine = null;
        }

        ResetCountdown();
    }

    private IEnumerator EvacuationCountdown()
    {
        currentTime = evacuationTime;
        while (currentTime > 0)
        {
            UpdateCountdownUI();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        CompleteEvacuation();
    }

    private void CompleteEvacuation()
    {
        ResetCountdown();
        OnLevelComplete?.Invoke(); // Вызываем ивент окончания уровня
    }

    private void ResetCountdown()
    {
        currentTime = evacuationTime;
        UpdateCountdownUI(reset: true);
    }

    private void UpdateCountdownUI(bool reset = false)
    {
        if (countdownText != null)
        {
            countdownText.text = reset ? "" : $"Эвакуация через: {Mathf.Ceil(currentTime)}";
        }
    }
}
