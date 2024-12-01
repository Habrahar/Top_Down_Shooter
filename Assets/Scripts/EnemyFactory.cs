using UnityEngine;

public class EnemyFactory
{
    public static GameObject CreateEnemy(
        EnemyConfig enemyConfig,
        Vector3 spawnPoint,
        EnemyWaveConfig currentWave)
    {
        if (enemyConfig == null || enemyConfig.enemyPrefab == null)
        {
            Debug.LogError("EnemyConfig or Enemy Prefab is null. Check your settings!");
            return null;
        }

        // Получаем врага из пула
        GameObject enemy = EnemyPool.Instance.GetEnemy(enemyConfig, spawnPoint, Quaternion.identity);

        // Настраиваем параметры врага
        Enemy_Controller enemyController = enemy.GetComponent<Enemy_Controller>();
        if (enemyController != null)
        {
            enemyController.SetParameters(enemyConfig, currentWave);
        }
        else
        {
            Debug.LogError("Enemy_Controller component is missing on the enemy prefab.");
        }

        return enemy;
    }
}

