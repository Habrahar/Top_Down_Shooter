using System;
using Level;
using New;
using UI;
using UnityEngine;
using TMPro;
using YG;

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
    public static Action OnSaveGame;

    private GameObject plprefab;

    [SerializeField] public WeaponConfig defaultWeapon;
    [SerializeField] public CharacterConfig defaultPlayer;

    [Header("Игрок")]
    public CharacterConfig playerPrefab;

    [Header("Сохранения")]
    [SerializeField]
    public SaveManager saver;

    private PlayerController playerController;
    private Transform playerSpawnPoint;
    private SavesYG saveData;

    public void EnemyKilled(EnemyConfig enemyType)
    {
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            OnSaveGame += SaveGame; 
            saver.Initialize(this);
            LoadGame();

            //LevelManager.StartNextLevel(currentLevel);
            windows.startWindow.Open();
            UpdateGold();
            windows.OpenGoldCurrency();
            LevelManager.SetCurrentLevel(currentLevel);
            SoundManager.Instance.PlayMusic("MainMenu_theme");
            SaveManager.Instance.Initialize(this); // Загружаем данные

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
    private void OnDestroy()
    {
        OnSaveGame -= SaveGame;
    }

    public void StartGame()
    {
        windows.CloseGoldCurrency();
        LevelManager.StartNextLevel(currentLevel);
        SetPlayer();
        windows.startWindow.Close();
        windows.goldcurrency.Close();
        
    }

    private void openLoseWindow()
    {
        windows.OpenGoldCurrency();
        DestroyPlayer();
        LevelManager.DespawnLevel();
        windows.OpenLoseWindow();
    }
    private void openWinWindow()
    {
        windows.OpenGoldCurrency();
        DestroyPlayer();
        LevelManager.SetLevelprogress(spawner.tmp_counter / spawner.counter);
        windows.winWindow.Open();
        if (spawner.tmp_counter != 0)
        {
            var tmp_reward= (int)(LevelManager.GetReward() * (spawner.tmp_counter/spawner.counter));
            windows.winWindow.SetReward(tmp_reward);
        }
        
        UpdateGold();
        OnLevelComplete();
        Destroy(plprefab);
        LevelManager.DespawnLevel();
        SaveGame();
        
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
            SaveGame();
    }

    public void EquipCurrentWeapon(PlayerController controller)
    {
        controller.EquipWeapon(currentWeapon);
        SaveGame();
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
        if (currentLevel == LevelManager.GetCurrentLevel())
        {
            currentLevel++;    
        }
        SaveGame();
        
    }

    public void UpdateGold()
    {
        GoldCount.text = Gold.ToString();
        SaveGame();
    }

    public void adRewardGold()
    {
        Gold += windows.goldWindowManager._rewardAmount;
        windows.goldWindowManager.OnWatchAdButtonClicked();
        UpdateGold();
        SaveGame();
    }
    public void SaveGame()
    {
        saver.SaveGame();
    }
    private void LoadGame()
    {
        saver.LoadGame();
    }

}
