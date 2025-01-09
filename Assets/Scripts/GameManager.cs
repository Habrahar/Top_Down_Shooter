using System;
using Level;
using New;
using UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentLevel = 0; // Начальный уровень
    public CameraController _cam;
    [SerializeField] public LevelManager LevelManager;
    [SerializeField] public WindowManager windows;
    [SerializeField] public WeaponConfig currentWeapon;
    [SerializeField] public int Gold;
    [SerializeField] public TextMeshProUGUI GoldCount;

    [Header("Игрок")]
    public GameObject playerPrefab;

    private PlayerController playerController;
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
            SpawnPlayer();
            UpdateGold();
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
        StartMenuWindow.playershopOpen += OpenCharachterShop;
        WeaponSelectionWindow.ApplyWeapon += SaveWeapon;
        PlayerController.playerDead += openLoseWindow;
        EnemySpawner.levelClear += openWinWindow;
    }

    private void OnDisable()
    {
        StartMenuWindow.playershopOpen += OpenCharachterShop;
        StartMenuWindow.gameStart -= StartGame;
        StartMenuWindow.shopOpen -= OpenShop;
        WeaponSelectionWindow.ApplyWeapon -= SaveWeapon;
        PlayerController.playerDead -= openLoseWindow;
        EnemySpawner.levelClear += openWinWindow;
    }

    public void StartGame()
    {
        LevelManager.StartNextLevel(currentLevel);
        SetPlayer();
        windows.startWindow.Close();
        
    }

    private void openLoseWindow()
    {
        LevelManager.DespawnLevel();
        windows.OpenLoseWindow();
    }
    private void openWinWindow()
    {
        LevelManager.DespawnLevel();
        Gold += LevelManager.GetReward();
        UpdateGold();
        windows.winWindow.Open();
        currentLevel++;
    }

    private void OpenShop()
    {
        windows.startWindow.Close();
        windows.weaponSelectionWindow.Open();
    }
    private void OpenCharachterShop()
    {
        windows.startWindow.Close();
        windows.playerShop.Open();
    }
    
    public void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            // Создаем игрока в нулевой позиции и с отключенным объектом
            playerPrefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            playerController = playerPrefab.GetComponent<PlayerController>();
            playerPrefab.SetActive(false); // Отключаем игрока до загрузки уровня
        }
        else
        {
            Debug.LogError("Не заданы prefab игрока!");
        }
    }

    public void SetPlayer()
    {
        playerSpawnPoint = LevelManager.getPlayerPos();
        // Проверяем, задана ли точка спавна
        if (playerSpawnPoint == null)
        {
            Debug.LogError("Точка спавна игрока не задана!");
            return;
        }
        
        // Перемещаем игрока в точку спавна и активируем его
        playerPrefab.transform.position = playerSpawnPoint.position;
        playerPrefab.transform.rotation = playerSpawnPoint.rotation;
        playerPrefab.SetActive(true);
        playerController.RestartPlayer();

        // Устанавливаем камеру и экипировку
        EquipCurrentWeapon(playerController);

        if (_cam != null)
        {
            _cam.player = playerController.transform;
        }
    }


    public void SaveWeapon(WeaponConfig config, int cost)
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

    public void UpdateGold()
    {
        GoldCount.text = Gold.ToString();
    }
    
}
