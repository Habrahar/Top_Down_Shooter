using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    private Transform playerTransform; // Ссылка на игрока
    private NavMeshAgent agent;        // Для движения врага
    private bool setNav = false;

    private void Update()
    {
        if (setNav && playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer > agent.stoppingDistance)
            {
                agent.SetDestination(playerTransform.position);
            }
            else
            {
                agent.ResetPath();
            }
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
                return; 
            }
        }

        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на враге.");
            return;
        }

        setNav = true;
    }
}
