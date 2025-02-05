using System.Collections.Generic;
using New;
using UnityEngine;

namespace Level
{

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] public List<LevelData> levelData; // Список префабов уровней
        [SerializeField] private ObjectPool<EnemyController> enemyPool; // Пул врагов
        [SerializeField] private GameManager gm;
        private GameObject currentLevelInstance; // Текущий активный уровень
        private int currentLevel;
        public Transform playerPos;
        private int tmp_reward;

        public void StartNextLevel(int currentLevelIndex)
        {
            if (levelData == null || levelData.Count == 0)
            {
                Debug.LogError("Список levelPrefabs пуст или не инициализирован!");
                return;
            }

            if (currentLevelIndex < 0 || currentLevelIndex >= levelData.Count)
            {
                Debug.LogError(
                    $"Индекс {currentLevelIndex} выходит за пределы списка (размер списка: {levelData.Count})!");
                return;
            }

            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
            }

            var levelPrefab = levelData[currentLevelIndex];
            if (levelPrefab == null)
            {
                Debug.LogError($"Префаб уровня с индексом {currentLevelIndex} равен NULL!");
                return;
            }

            currentLevel = currentLevelIndex;
            currentLevelInstance = Instantiate(levelPrefab.levelPrefab);

            InitializeLevel(currentLevelInstance);

            gm.SetPlayer();
            gm.windows.startWindow.Close();

        }



        private void InitializeLevel(GameObject level)
        {
            var levelComponent = level.GetComponent<LevelComponent>();
            tmp_reward = levelComponent.GoldReward;
            if (levelComponent == null)
            {
                Debug.LogError("На уровне отсутствует компонент LevelComponent!");
                return;
            }

            playerPos = levelComponent.playerSpawnPoint;
            DespawnEnemies();
            SpawnEnemies(levelComponent);

        }

        public Transform getPlayerPos()
        {
            return playerPos;
        }

        private void DespawnEnemies()
        {
            EnemySpawner.Instance.ReturnAllEnemy();
            EnemySpawner.Instance.resetDeadCount();
        }

        public int GetReward()
        {
            return tmp_reward;
        }

        private void SpawnEnemies(LevelComponent level)
        {
            for (int i = 0; i < level.enemySpawnPoints.Count; i++)
            {
                var config = level.enemySpawnPoints[i].enemyConfig;
                var spawnPoint = level.enemySpawnPoints[i].spawnPoint;

                // Получаем врага из спавнера
                var enemy = EnemySpawner.Instance.GetEnemy(config);
                enemy.transform.position = spawnPoint.position;
                enemy.transform.rotation = spawnPoint.rotation;
                enemy.Initialize(config);
                enemy.hpBar.SetHealth(enemy.MaxHealth);
            }

            EnemySpawner.Instance.setDeadCount(level.enemySpawnPoints.Count);
        }

        public void DespawnLevel()
        {
            DespawnEnemies();
            Destroy(currentLevelInstance);
        }

        public List<LevelData> GetLevels()
        {
            return levelData;
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        public void SetCurrentLevel(int level)
        {
            currentLevel = level;
        }

        public void SetLevelprogress(float progres)
        {
            if (progres > levelData[currentLevel].progress)
            {
                levelData[currentLevel].progress = progres;
            }
        }

        public float GetLevelProgress(int level)
        {
            
                return levelData[level].progress;

        }
    }
}