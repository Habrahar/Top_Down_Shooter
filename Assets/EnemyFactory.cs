using UnityEngine;

public class EnemyFactory
{
    // Метод создания врагов
    public static GameObject CreateEnemy(
        EnemyConfig enemyConfig,
        Vector3 spawnPoint,
        float healthMultiplier,
        float damageMultiplier)
    {
        if (enemyConfig == null || enemyConfig.enemyPrefab == null)
        {
            Debug.LogError("EnemyConfig or Enemy Prefab is null. Check your settings!");
            return null;
        }

        // Создаём врага
        GameObject enemy = Object.Instantiate(enemyConfig.enemyPrefab, spawnPoint, Quaternion.identity);

        // Настраиваем врага
        Enemy_Controller enemyController = enemy.GetComponent<Enemy_Controller>();
        if (enemyController != null)
        {
            enemyController.SetParameters(enemyConfig, healthMultiplier, damageMultiplier);
            //enemyController.SetComponents(); Сделать инициализацию движения
        }
        else
        {
            Debug.LogError("Enemy_Controller component is missing on the enemy prefab.");
        }

        return enemy;
    }
}
