using UnityEngine;


namespace Level
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Game/LevelData")]
    public class LevelData : ScriptableObject
    {
        public GameObject levelPrefab; // Префаб уровня
        public float progress = 0f; 
        
    }
}