using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    private BaseEnemy _enemy;
    private NavMeshAgent _navMeshAgent;
    private StateMachine _stateMachine;
    private Transform _playerTarget;


    public void Initialize(EnemyConfig config)
    {
        _enemy = new BaseEnemy();
        _enemy.Initialize(config);

        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent != null)
        {
            _navMeshAgent.speed = config.Speed;
        }
        
        _playerTarget = GameObject.FindGameObjectWithTag("Player")?.transform;

        _stateMachine = new StateMachine();

        _stateMachine.ChangeState(new MoveToTargetState(this, _playerTarget));
    }

    void Update()
    {
        _stateMachine?.Update();    
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

    public void MoveTo(Vector3 target)
    {
        if (_enemy != null && _navMeshAgent != null)
        {
            _enemy.MoveTo(target);
            _navMeshAgent.SetDestination(target);
        }
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}

