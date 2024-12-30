using System;
using Level;
using New;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentLevel = 0; // Начальный уровень
    public CameraController _cam;
    [SerializeField] public LevelManager LevelManager;
    [SerializeField] public WindowManager windows;
    [SerializeField] private WeaponConfig currentWeapon;
    protected int Gold;

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
            //LevelManager.StartNextLevel(currentLevel);
            windows.startWindow.Open();
            //SpawnPlayer();
        }
        else
        {
            Destroy(gameObject); // Если уже есть экземпляр GameManager, уничтожаем текущий
        }
    }

    private void OnEnable()
    {
        StartMenuWindow.gameStart += StartGame;
        StartMenuWindow.shopOpen += OpenShop;
        WeaponSelectionWindow.ApplyWeapon += SaveWeapon;
    }

    private void OnDisable()
    {
        StartMenuWindow.gameStart -= StartGame;
        StartMenuWindow.shopOpen -= OpenShop;
        WeaponSelectionWindow.ApplyWeapon -= SaveWeapon;
    }

    private void StartGame()
    {
        LevelManager.StartNextLevel(currentLevel);
        SpawnPlayer();
        windows.startWindow.Close();
        
    }

    private void OpenShop()
    {
        windows.startWindow.Close();
        windows.weaponSelectionWindow.Open();
    }
    public void SpawnPlayer()
    {
        playerSpawnPoint = LevelManager.getPlayerPos();
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            
            
            GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            var playerController = player.GetComponent<PlayerController>();
            EquipCurrentWeapon(playerController);
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

    public void SaveWeapon(WeaponConfig config)
    {
        currentWeapon = config;
    }

    public void EquipCurrentWeapon(PlayerController controller)
    {
        controller.EquipWeapon(currentWeapon);
    }

    public void StartLevel()
    {
        
        
    }

    private void OnLevelComplete()
    {
        
        currentLevel++;
        
    }
    
}
