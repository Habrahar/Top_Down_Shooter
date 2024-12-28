using System;
using Level;
using New;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentLevel = 0; // Начальный уровень
    public CameraController _cam;
    [SerializeField] public LevelManager LevelManager;

    [Header("Игрок")]
    public GameObject playerPrefab;
    private Transform playerSpawnPoint;
    public void EnemyKilled(EnemyConfig enemyType)
    {
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LevelManager.StartNextLevel(currentLevel);
            SpawnPlayer();
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
    
    public void SpawnPlayer()
    {
        playerSpawnPoint = LevelManager.getPlayerPos();
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            
            
            GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            
            if (_cam != null)
            {
                _cam.player = player.transform;
            }
        }
        else
        {
            Debug.LogError("Не заданы prefab игрока или точка спавна!");
        }
    }


    public void StartWave()
    {
        
        
    }

    private void OnWaveComplete()
    {
        // Если волна завершена, увеличиваем уровень и запускаем следующую волну
        currentLevel++;
        StartWave(); // Запускаем следующую волну
    }
    
}
