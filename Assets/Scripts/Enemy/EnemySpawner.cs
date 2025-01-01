using UnityEngine;
using System.Collections.Generic;

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

        private Dictionary<EnemyConfig, ObjectPool<EnemyController>> enemyPools;

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

        public EnemyController GetEnemy(EnemyConfig config)
        {
            if (!enemyPools.TryGetValue(config, out var pool))
            {
                // Если пула нет, создаем его
                var prefab = config.Prefab.GetComponent<EnemyController>();
                pool = new ObjectPool<EnemyController>(prefab, initialPoolSize, transform);
                enemyPools[config] = pool;
            }

            var enemy = pool.Get();
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
                        pool.Return(enemy); // Возвращаем в пул только активных
                    }
                }
            }
        }


    }
}