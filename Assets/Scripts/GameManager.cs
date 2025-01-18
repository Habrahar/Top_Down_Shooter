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
    [SerializeField] public EnemySpawner spawner;
    private GameObject plprefab;

    [SerializeField] private WeaponConfig defaultWeapon;
    [SerializeField] private CharacterConfig defaultPlayer;

    [Header("Игрок")]
    public CharacterConfig playerPrefab;

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
        EvacuationZone.OnLevelComplete += openWinWindow;
        AdManager.OnRewardGoldGranted += adRewardGold;
    }

    private void OnDisable()
    {
        StartMenuWindow.playershopOpen += OpenCharachterShop;
        StartMenuWindow.gameStart -= StartGame;
        StartMenuWindow.shopOpen -= OpenShop;
        WeaponSelectionWindow.ApplyWeapon -= SaveWeapon;
        PlayerController.playerDead -= openLoseWindow;
        EvacuationZone.OnLevelComplete -= openWinWindow;
        AdManager.OnRewardGoldGranted -= adRewardGold;
    }

    public void StartGame()
    {
        LevelManager.StartNextLevel(currentLevel);
        SetPlayer();
        windows.startWindow.Close();
        
    }

    private void openLoseWindow()
    {
        DestroyPlayer();
        LevelManager.DespawnLevel();
        windows.OpenLoseWindow();
    }
    private void openWinWindow()
    {
        DestroyPlayer();
        windows.winWindow.Open();
        if (spawner.tmp_counter != 0)
        {
            Gold += LevelManager.GetReward() / (spawner.tmp_counter/spawner.counter);    
        }
        
        UpdateGold();
        currentLevel++;
        Destroy(plprefab);
        LevelManager.DespawnLevel();
        
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

    public void DestroyPlayer()
    {
        playerController.hpBar.destroyHP();
        Destroy(plprefab);
    }
    public void SetPlayer()
    {
        if (playerPrefab != null)
        {
            // Создаем игрока в нулевой позиции и с отключенным объектом
            plprefab = Instantiate(playerPrefab.modelPrefab, Vector3.zero, Quaternion.identity);
            playerController = plprefab.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Не заданы prefab игрока!");
        }

        playerController.MaxHealth = playerPrefab.MaxHp;
        playerController.moveSpeed = playerPrefab.speed;
        playerController.detectionRadius = playerPrefab.radiusAttack;
        playerSpawnPoint = LevelManager.getPlayerPos();
        // Проверяем, задана ли точка спавна
        if (playerSpawnPoint == null)
        {
            Debug.LogError("Точка спавна игрока не задана!");
            return;
        }
        
        // Перемещаем игрока в точку спавна и активируем его
        plprefab.transform.position = playerSpawnPoint.position;
        plprefab.transform.rotation = playerSpawnPoint.rotation;
        plprefab.SetActive(true);

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

    public void resetProgress()
    {
        currentLevel = 0;
        Gold = 10000;
        UpdateGold();
        ResetWeapon();
        ResetPlayer();

    }

    private void ResetPlayer()
    {
        defaultPlayer = playerPrefab;
        windows.playerShop.ResetAllCharachters(defaultPlayer);
    }

    private void ResetWeapon()
    {
        defaultWeapon = currentWeapon;
        windows.weaponSelectionWindow.ResetAllWeapon(defaultWeapon);
    }

    private void OnLevelComplete()
    {
        
        currentLevel++;
        
    }

    public void UpdateGold()
    {
        GoldCount.text = Gold.ToString();
    }

    public void adRewardGold()
    {
        Gold += windows.goldWindowManager._rewardAmount;
        windows.goldWindowManager.OnWatchAdButtonClicked();
        UpdateGold();
    }
    
}
