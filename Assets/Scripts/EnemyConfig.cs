using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public int baseHealth = 100;
    public float moveSpeed = 3f;
    public float detectionRadius = 10f; // Радиус обнаружения игрока
    public float attackRange = 2f; // Расстояние до игрока для атаки
    public float healthMultiplier = 1f; // Множитель для здоровья
    public float speedMultiplier = 1f; // Множитель для скорости

    // Этот метод будет использоваться для вычисления окончательных характеристик
    public int GetHealth() => Mathf.RoundToInt(baseHealth * healthMultiplier);
    public float GetMoveSpeed() => moveSpeed * speedMultiplier;
}
