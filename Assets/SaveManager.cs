using DefaultNamespace;
using UnityEngine;
using YG;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private GameManager gameManager;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
        LoadGame();
    }

    public void SaveGame()
    {
        if (gameManager == null) return;

        SavesYG saveData = YandexGame.savesData;

        // Сохраняем текущие параметры
        saveData.currentLevel = gameManager.currentLevel;
        saveData.gold = gameManager.Gold;
        saveData.CurrentWeapon = gameManager.currentWeapon;
        saveData.CurrentCharachter = gameManager.playerPrefab;
        saveData.rewAdShowCount = gameManager.windows.goldWindowManager.countWatched;
        saveData.rewAdShowCooldown = gameManager.windows.goldWindowManager._cooldownMinutes;
        
        
        saveData.levelProgress.Clear();
        foreach (var level in gameManager.LevelManager.levelData) 
        {
            saveData.levelProgress.Add(new LevelProgressData { levelName = level.name, progress = level.progress });
        }



       

        YandexGame.SaveProgress();
    }

    public void LoadGame()
    {
        SavesYG saveData = YandexGame.savesData;

        gameManager.currentLevel = saveData.currentLevel;
        gameManager.Gold = saveData.gold;

        gameManager.currentWeapon = saveData.CurrentWeapon ?? gameManager.currentWeapon;
        gameManager.playerPrefab = saveData.CurrentCharachter ?? gameManager.playerPrefab;

        if (gameManager.windows?.goldWindowManager != null)
        {
            gameManager.windows.goldWindowManager.countWatched = saveData.rewAdShowCount;
            gameManager.windows.goldWindowManager._cooldownMinutes = saveData.rewAdShowCooldown;
        }
        else
        {
            Debug.LogError("goldWindowManager is null!");
        }

        gameManager.UpdateGold();

        if (saveData.levelProgress == null)
        {
            Debug.LogError("saveData.levelProgress is null! Skipping level loading.");
            return;
        }

        foreach (var level in gameManager.LevelManager.levelData)
        {
            var savedLevel = saveData.levelProgress.Find(l => l.levelName == level.name);
            if (savedLevel != null)
            {
                level.progress = savedLevel.progress;
            }
            else
            {
                Debug.LogWarning($"Level {level.name} not found in save data.");
            }
        }
    }

}
