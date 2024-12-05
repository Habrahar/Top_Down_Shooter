using UnityEngine;

public class BaseEnemy : IDamageable, IMovable
{
    protected int health;
    protected float speed;
    public bool IsAlive => health > 0;
    protected Vector3 currentTarget;


    public void Initialize(EnemyConfig data)
    {
        health = data.Health;
        Debug.Log("Enemy initialized with health: " + health);
    }

    public void MoveTo(Vector3 target)
    {
        currentTarget = target;
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
