using UnityEngine;
using System;
using System.Collections;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance { get; private set; }
    public static event Action RoundComplete;
    public WaveConfig waveConfig; // Ссылка на конфиг волн
    private int enemiesRemainingInWave; // Сколько врагов осталось в текущей волне
    public GameObject[] spawnPoints; // Точка спауна врагов

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        Enemy_Controller.OnDeath += OnEnemyDeath;
    }

    private void OnDisable()
    {
        Enemy_Controller.OnDeath -= OnEnemyDeath;
    }

    // Запуск волны
    public void StartWave(int level)
    {
        if (level >= waveConfig.waves.Length)
        {
            Debug.LogError("Уровень превышает количество доступных волн!");
            return;
        }

        EnemyWaveConfig currentWave = waveConfig.waves[level];
        enemiesRemainingInWave = currentWave.enemyCount;
        
        
        
        // Спавним врагов
        StartCoroutine(SpawnEnemies(currentWave));
    }

    // Корутин для спавна врагов
    // В методе SpawnEnemies
    private IEnumerator SpawnEnemies(EnemyWaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            Vector3 spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;


            GameObject enemy = EnemyFactory.CreateEnemy(
                currentWave.enemyConfig,
                spawnPoint,
                currentWave);

            if (enemy == null)
            {
                Debug.LogError("Enemy creation failed. Skipping...");
            }

            yield return new WaitForSeconds(1f);
        }
    }

    
    

    // Когда враг умирает, уменьшаем количество оставшихся врагов
    private void OnEnemyDeath()
    {
        enemiesRemainingInWave--;
        if (enemiesRemainingInWave <= 0)
        {
            // Все враги мертвы, завершаем волну
            OnWaveComplete();
        }
    }

    // Завершение волны
    private void OnWaveComplete()
    {
        Debug.Log("Волна завершена!");
        RoundComplete?.Invoke();
        // Тут можно вызвать ивент или обработать завершение волны
    }
}
