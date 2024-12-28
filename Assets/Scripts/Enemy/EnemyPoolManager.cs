using System.Collections.Generic;
using UnityEngine;

namespace New
{
    public class EnemyPoolManager
    {
        private static readonly Dictionary<EnemyConfig, ObjectPool<EnemyController>> enemyPools =
            new Dictionary<EnemyConfig, ObjectPool<EnemyController>>();

        public static EnemyController GetEnemy(EnemyConfig config, Transform parent = null)
        {
            if (!enemyPools.TryGetValue(config, out var pool))
            {
                // Создаем новый пул для этого EnemyConfig
                var prefab = config.Prefab.GetComponent<EnemyController>();
                pool = new ObjectPool<EnemyController>(prefab, 10, parent);
                enemyPools[config] = pool;
            }

            return pool.Get();
        }

        public static void ReturnEnemy(EnemyConfig config, EnemyController enemy)
        {
            if (enemyPools.TryGetValue(config, out var pool))
            {
                pool.Return(enemy);
            }
            else
            {
                Debug.LogWarning("Попытка вернуть врага в несуществующий пул!");
            }
        }
    }
}