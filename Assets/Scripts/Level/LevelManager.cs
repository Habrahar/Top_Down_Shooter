using System.Collections.Generic;
using New;
using UnityEngine;

namespace Level
{

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> levelPrefabs; // Список префабов уровней
        [SerializeField] private ObjectPool<EnemyController> enemyPool; // Пул врагов
        
        private GameObject currentLevelInstance; // Текущий активный уровень
        public Transform playerPos;

        public void StartGame(int currentLevelIndex)
        {
            if (levelPrefabs == null || levelPrefabs.Count == 0)
            {
                Debug.LogError("Список levelPrefabs пуст или не инициализирован!");
                return;
            }

            if (currentLevelIndex < 0 || currentLevelIndex >= levelPrefabs.Count)
            {
                Debug.LogError($"Индекс {currentLevelIndex} выходит за пределы списка (размер списка: {levelPrefabs.Count})!");
                return;
            }

            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
            }

            var levelPrefab = levelPrefabs[currentLevelIndex];
            if (levelPrefab == null)
            {
                Debug.LogError($"Префаб уровня с индексом {currentLevelIndex} равен NULL!");
                return;
            }

            currentLevelInstance = Instantiate(levelPrefab);

            InitializeLevel(currentLevelInstance);
        }
        public void StartNextLevel(int currentLevelIndex)
        {
            if (levelPrefabs == null || levelPrefabs.Count == 0)
            {
                Debug.LogError("Список levelPrefabs пуст или не инициализирован!");
                return;
            }

            if (currentLevelIndex < 0 || currentLevelIndex >= levelPrefabs.Count)
            {
                Debug.LogError($"Индекс {currentLevelIndex} выходит за пределы списка (размер списка: {levelPrefabs.Count})!");
                return;
            }

            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
            }

            var levelPrefab = levelPrefabs[currentLevelIndex];
            if (levelPrefab == null)
            {
                Debug.LogError($"Префаб уровня с индексом {currentLevelIndex} равен NULL!");
                return;
            }

            currentLevelInstance = Instantiate(levelPrefab);

                InitializeLevel(currentLevelInstance);
        }



      private void InitializeLevel(GameObject level)
      {
          var levelComponent = level.GetComponent<LevelComponent>();
      
          if (levelComponent == null)
          {
              Debug.LogError("На уровне отсутствует компонент LevelComponent!");
              return;
          }

          playerPos = levelComponent.playerSpawnPoint;
      
          SpawnEnemies(levelComponent);
          
      }

      public Transform getPlayerPos()
      {
          return playerPos;
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
          }
      }
        
    }
}