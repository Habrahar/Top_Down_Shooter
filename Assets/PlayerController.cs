using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public void Start()
    {
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
}
