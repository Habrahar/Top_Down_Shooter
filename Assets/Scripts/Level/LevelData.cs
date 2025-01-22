using UnityEngine;


namespace Level
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Game/LevelData")]
    public class LevelData : ScriptableObject
    {
        public GameObject levelPrefab; // Префаб уровня
        public Transform playerSpawnPoint; // Точка спавна игрока
        public EnemySpawnInfo[] enemySpawnPoints; // Информация о точках спавна 
        
    }

    [System.Serializable]
    public class EnemySpawnInfo
    {
        public Transform position;
        public EnemyConfig enemyConfig; // Конфигурация врага
    }
        
}