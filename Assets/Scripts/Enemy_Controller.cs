using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    public EnemyConfig enemyConfig; // Ссылка на конфиг врага
    private int currentHealth;
    private Transform playerTransform; // Ссылка на игрока
    private bool isPlayerInSight = false; // Флаг, что игрок в зоне видимости
    private NavMeshAgent agent; // Для движения врага
    public GameObject damagePopupPrefab;
    


    private void Start()
    {
        // Получаем ссылку на игрока
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Настройка характеристик врага из конфигурации
        currentHealth = enemyConfig.GetHealth();
        
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

    // Метод получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
