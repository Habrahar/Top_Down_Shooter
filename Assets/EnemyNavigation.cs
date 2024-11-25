using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    private bool isPlayerInSight = false; // Флаг, что игрок в зоне видимости
    private Transform playerTransform; // Ссылка на игрока
    private NavMeshAgent agent; // Для движения врага
    
    private float speed = 1f;

    private bool setNav = false;

    // Update is called once per frame
    public void Update()
    {
        if(setNav == true)
        {
            // Проверка радиуса обнаружения
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= agent.stoppingDistance)
            {
                isPlayerInSight = true;
                MoveTowardsPlayer();
                
            }
            else
            {
                isPlayerInSight = false;
            }

            // Если игрок в радиусе атаки, можно добавить логику для атаки
            if (distanceToPlayer <= agent.stoppingDistance)
            {
                // Добавьте логику для атаки (удар или стрельба и т.д.)
            }
        }
        
    }

    private void MoveTowardsPlayer()
    {
        if (isPlayerInSight)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    public void SetNavigation()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (playerTransform == null)
            {
                Debug.LogError("Игрок не найден! Объект не может двигаться.");
                return; // Выход из метода, если игрок не найден
            }
        }

        // Инициализация NavMeshAgent для движения врага
        agent = GetComponent<NavMeshAgent>(); 
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на враге.");
        }
        setNav = true;
    }
}
