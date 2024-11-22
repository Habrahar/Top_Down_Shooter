using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy_Controller : MonoBehaviour
{
    public EnemyConfig enemyConfig; // Ссылка на конфиг врага
    private float currentHealth;
    private float maxHealth;
    private Transform playerTransform; // Ссылка на игрока
    private Transform damagePoint;
    private bool isPlayerInSight = false; // Флаг, что игрок в зоне видимости
    private NavMeshAgent agent; // Для движения врага
    public static event Action<float, Vector3> OnDamageTaken;

    [SerializeField]
    public EnemyHealthBar _hpBar;

    


    private void Start()
    {
        
        // Получаем ссылку на игрока
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //Точка спавна урона
        damagePoint = gameObject.transform.Find("DamagePoint");
        
        // Настройка характеристик врага из конфигурации
        currentHealth = enemyConfig.GetHealth();

        maxHealth = currentHealth;

        //передаем значение ХП в слайдер
        _hpBar.UpdateHealthBar(currentHealth, maxHealth);

        // Инициализация NavMeshAgent для движения врага
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyConfig.GetMoveSpeed();
    }

    private void Update()
    {
        // Проверка радиуса обнаружения
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= enemyConfig.detectionRadius)
        {
            isPlayerInSight = true;
            MoveTowardsPlayer();
        }
        else
        {
            isPlayerInSight = false;
        }

        // Если игрок в радиусе атаки, можно добавить логику для атаки
        if (distanceToPlayer <= enemyConfig.attackRange)
        {
            // Добавьте логику для атаки (удар или стрельба и т.д.)
        }
    }

    // Двигаемся в сторону игрока
    private void MoveTowardsPlayer()
    {
        if (isPlayerInSight)
        {
            agent.SetDestination(playerTransform.position);
        }
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
    
    
    private void Die()
    {
        Destroy(gameObject); // Уничтожаем врага
    }
}
