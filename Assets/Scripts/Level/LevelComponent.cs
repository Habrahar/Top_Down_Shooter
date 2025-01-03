using System.Collections.Generic;
using UnityEngine;
    
namespace Level
{
    [System.Serializable]
    public class EnemySpawnPoint
    {
        public Transform spawnPoint;  // Точка спавна врага
        public EnemyConfig enemyConfig; // Конфиг врага
    }

    public class LevelComponent : MonoBehaviour
    {
        public Transform playerSpawnPoint; // Точка спавна игрока
        public int GoldReward;
        public List<EnemySpawnPoint> enemySpawnPoints; // Точки спавна врагов
    }
}