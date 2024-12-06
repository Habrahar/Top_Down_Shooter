using UnityEngine;

public class BaseEnemy
{
    protected float health;
    protected float damage;
    protected float speed;

    public void Initialize(EnemyConfig data)
    {
        health = data.Health;
        damage = data.Damage;
        speed = data.Speed;
        Debug.Log("Enemy initialized with health: " + health);
        Debug.Log("Enemy initialized with Damage: " + damage);
        Debug.Log("Enemy initialized with Speed: " + speed);
    }

}
