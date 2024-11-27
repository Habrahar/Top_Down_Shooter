using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy_Controller : MonoBehaviour
{
    private float currentHealth = 1f;
    private float maxHealth = 1f;
    private float damage = 1f;
    private float speed = 1f;
    
    private Transform damagePoint;
    
    
    public static event Action<float, Vector3> OnDamageTaken;
    public static event Action OnDeath;

    [SerializeField]
    public EnemyHealthBar _hpBar;

    [SerializeField]
    public EnemyNavigation _navigation;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnDamageTaken?.Invoke(damage, damagePoint.position);

        _hpBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetParameters(EnemyConfig enemyConfig, EnemyWaveConfig currentWave)
    {
        if (enemyConfig == null)
        {
            Debug.LogError("EnemyConfig is null! Please check the configuration.");
            return;
        }

        damagePoint = transform.Find("DamagePoint");        

        maxHealth = enemyConfig.GetHealth();
        damage = enemyConfig.GetDamage();
        _navigation.SetNavigation(enemyConfig);

        // Умножаем здоровье и урон на множители
        maxHealth *= currentWave.healthMultiplier;
        currentHealth = maxHealth;  // Устанавливаем текущее здоровье
        damage *= currentWave.damageMultiplier;

        // Выводим информацию о параметрах врага
        Debug.Log($"Enemy initialized with Health: {currentHealth}, Damage: {damage}");
    }

    private void Die()
    {
        // Когда враг умирает, вызываем событие OnDeath
        OnDeath?.Invoke();
        Destroy(gameObject); // Уничтожаем врага
    }
}
