using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentLevel = 0; // Начальный уровень
    public CameraController _cam;

    [Header("Игрок")]
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Спавн игрока в стартовой точке
            SpawnPlayer();
            StartWave();
        }
        else
        {
            Destroy(gameObject); // Если уже есть экземпляр GameManager, уничтожаем текущий
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    
    private void SpawnPlayer()
    {
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            // Спавним игрока в заданной точке
            GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

            // Обновляем ссылку на игрока в CameraController
            if (_cam != null)
            {
                _cam.player = player.transform; // Передаем ссылку на трансформ игрока
                //PlayerLocator.Locate();
            }
        }
        else
        {
            Debug.LogError("Не заданы prefab игрока или точка спавна!");
        }
    }


    public void StartWave()
    {
        // Запуск волны для текущего уровня
        
    }

    private void OnWaveComplete()
    {
        // Если волна завершена, увеличиваем уровень и запускаем следующую волну
        currentLevel++;
        StartWave(); // Запускаем следующую волну
    }
    
}
