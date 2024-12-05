using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfig; // Конфиг
    [SerializeField] private Transform spawnPoint;    // Точка спавна

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        // Создаем объект врага
        GameObject enemyObject = Instantiate(enemyConfig.Prefab, spawnPoint.position, Quaternion.identity);

        // Настраиваем EnemyController
        if (enemyObject.TryGetComponent<EnemyController>(out var enemyController))
        {
            enemyController.Initialize(enemyConfig);
        }
        else
        {
            Debug.LogError("EnemyController not found on prefab!");
        }
    }
}
