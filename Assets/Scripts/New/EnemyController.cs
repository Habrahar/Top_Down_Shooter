using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour, IDamageable
{
    private BaseEnemy _enemy;
    private Transform _playerTarget;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return _navMeshAgent;
    }

    public Transform GetPlayerTarget()
    {
        if (_playerTarget == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _playerTarget = player.transform;
            }
            else
            {
                Debug.LogWarning("Player not found in the scene!");
            }
        }
        return _playerTarget;
    }
    public void Initialize(EnemyConfig config)
    {
        _enemy = new BaseEnemy();
        _enemy.Initialize(config);
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (_enemy != null)
        {
            _enemy.TakeDamage(damage);

            if (!_enemy.IsAlive)
            {
                HandleDeath();
            }
        }
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}

