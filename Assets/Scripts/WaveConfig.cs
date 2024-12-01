using UnityEngine;

[System.Serializable]
public class EnemyWaveConfig
{
    public EnemyConfig enemyConfig;
    public int enemyCount;
    public float healthMultiplier;
    public float damageMultiplier;
    
}


[CreateAssetMenu(fileName = "WaveConfig", menuName = "WaveConfig", order = 0)]
public class WaveConfig : ScriptableObject
{
    public EnemyWaveConfig[] waves; // Массив с волнами
}
