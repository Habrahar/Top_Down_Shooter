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

    [SerializeField]
    public EnemyParameters _parameters;


    private void Start(){
    
    }   

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

    // В классе Enemy_Controller
    


    public void SetParameters(EnemyConfig enemyConfig, float healthMul, float damageMul)
    {
        if (enemyConfig == null)
        {
            Debug.LogError("EnemyConfig is null! Please check the configuration.");
            return;
        }

        // Настройка характеристик врага из конфигурации
        // agent.speed = enemyConfig.GetMoveSpeed(); Добавить потом передачу скорости или сделать дефолтную
        damagePoint = transform.Find("DamagePoint");

        currentHealth = enemyConfig.GetHealth();
        damage = enemyConfig.GetDamage();
        _navigation.SetNavigation();

        // Умножаем здоровье и урон на множители
        maxHealth = currentHealth * healthMul;
        currentHealth = maxHealth;  // Устанавливаем текущее здоровье
        damage *= damageMul;

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
