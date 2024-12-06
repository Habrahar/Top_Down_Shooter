using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : IDamageable 
{
    protected int health;
    protected float speed;
    public bool IsAlive => health > 0;
    protected Vector3 currentTarget;
    private Transform _playerTarget;


    public void Initialize(EnemyConfig data)
    {
        health = data.Health;
        Debug.Log("Enemy initialized with health: " + health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        // Тут можно добавить вызов события
    }
}
