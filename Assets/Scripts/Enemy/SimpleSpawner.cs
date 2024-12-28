using New;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    //Спавнеор на точку
    [SerializeField] private EnemyConfig enemyConfig; // Конфиг
    [SerializeField] private EnemyConfig spawnDelay; // задержка перед спавнов
    [SerializeField] private EnemyConfig spawnInterval; // интервал спавна
    [SerializeField] private Transform spawnPoint;    // Точка спавна
    [SerializeField] private int count;

    private void Start()
    {
        
    }

    public void InitializeEnemies()
    {
        SpawnEnemy(spawnPoint.position);
    }

    public void SpawnEnemy(Vector3 point)
    {
        // Создаем объект врага
        GameObject enemyObject = Instantiate(enemyConfig.Prefab,point, Quaternion.identity);

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
