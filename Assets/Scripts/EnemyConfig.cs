using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public GameObject enemyPrefab; // Добавляем ссылку на префаб врага
    public int baseHealth = 100;
    public float damage = 2f; // Урон врага
    public float moveSpeed = 3f;
    public float attackRange = 2f; // Расстояние до игрока для атаки
    

    // Этот метод будет использоваться для вычисления окончательных характеристик
    public int GetHealth() => baseHealth;
    public float GetMoveSpeed() => moveSpeed;
    public float GetDamage() => damage;
    public float GetAttackRange() => attackRange;
}
