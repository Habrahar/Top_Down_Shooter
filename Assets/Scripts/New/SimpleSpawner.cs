using New;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfig; // Конфиг
    [SerializeField] private Transform[] spawnPoint;    // Точка спавна
    [SerializeField] private int count;

    private void Start()
    {
        for (int i = 0; i <= count; i++)
        {
            int randomNumber = Random.Range(1, spawnPoint.Length);
            SpawnEnemy(spawnPoint[randomNumber].position);
        }
    
    }

    public void SpawnEnemy(Vector3 point)
    {
        // Создаем объект врага
        GameObject enemyObject = Instantiate(enemyConfig.Prefab,point, Quaternion.identity);

        // Настраиваем EnemyController
        if (enemyObject.TryGetComponent<EnemyController>(out var enemyController))
        {
            enemyController.Initialize(enemyConfig);
            var player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                enemyController.InitializeTarget(player.GetComponent<IDamageable>());
            }

        }
        else
        {
            Debug.LogError("EnemyController not found on prefab!");
        }
    }
}
