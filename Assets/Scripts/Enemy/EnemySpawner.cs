using UnityEngine;
using System.Collections.Generic;
using System;
namespace New
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance { get; private set; }
        [System.Serializable]
        public class SpawnPoint
        {
            public Transform spawnPosition;
            public EnemyConfig enemyConfig;
        }
       
        public int initialPoolSize = 5;
        public float counter;
        public float tmp_counter;
        private Dictionary<EnemyConfig, ObjectPool<EnemyController>> enemyPools;
        public static event Action<int> levelClear; // Событие обновления патронов

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                enemyPools = new Dictionary<EnemyConfig, ObjectPool<EnemyController>>();
            }
            else
            {
                Destroy(gameObject); // Убедимся, что существует только один экземпляр
            }
        }
        private void OnEnable()
        {
            EnemyController.dieTrigger += deadCount;
        }

        private void OnDisable()
        {
            EnemyController.dieTrigger -= deadCount;
        }

        public void setDeadCount(int levelCount)
        {
            counter = levelCount;
        }
        private void deadCount()
        {
            tmp_counter++;
            Debug.Log((tmp_counter));
        }
        public void resetDeadCount()
        {
            tmp_counter = 0;
        }

        public EnemyController GetEnemy(EnemyConfig config)
        {
            if (!enemyPools.TryGetValue(config, out var pool))
            {
                var prefab = config.Prefab.GetComponent<EnemyController>();
                pool = new ObjectPool<EnemyController>(prefab, initialPoolSize, transform);
                enemyPools[config] = pool;
            }
        
            var enemy = pool.Get();
            if (enemy.agent != null)
            {
                enemy.agent.enabled = false;
                enemy.agent.enabled = true; // Перезапускаем агент
            }

            enemy.SetPool(pool);
            return enemy;
        }


        public void ReturnEnemy(EnemyController enemy, EnemyConfig config)
        {
            if (enemyPools.TryGetValue(config, out var pool))
            {
                pool.Return(enemy);
            }
        }

        public void ReturnAllEnemy()
        {
            foreach (var poolEntry in enemyPools)
            {
                var pool = poolEntry.Value;
                
                foreach (var enemy in pool.Objects)
                {
                    if (enemy.gameObject.activeSelf) // Проверяем, активен ли враг
                    {
                        enemy.hpBar.destroyHP();
                        pool.Return(enemy); // Возвращаем в пул только активных
                    }
                }
            }
        }


    }
}