using UnityEngine;
using UnityEngine.AI;

namespace Top_Down_Shooter.Assets.Scripts.New.StateMachine
{
    public class MoveState : BaseState
    {
        private NavMeshAgent _navMeshAgent;
        private Transform _playerTarget;

        public override void EnterState(StateManager stateManager)
        {
            var enemy = stateManager.GetEnemyController();
            _navMeshAgent = enemy.GetNavMeshAgent();
            _playerTarget = enemy.GetPlayerTarget();

            if (_playerTarget != null && _navMeshAgent != null)
            {
                _navMeshAgent.SetDestination(_playerTarget.position);

                // Проверка пути
                if (_navMeshAgent.pathPending)
                {
                    Debug.Log("Path is pending...");
                }
                else if (_navMeshAgent.hasPath)
                {
                    Debug.Log("Path found!");
                }
                else
                {
                    Debug.LogWarning("No path to destination!");
                }
            }
            else
            {
                Debug.LogWarning("MoveState: Player or NavMeshAgent not found.");
            }
        }

        public override void UpdateState(StateManager stateManager)
        {
            if (_playerTarget == null || _navMeshAgent == null) return;

            float distanceToTarget = Vector3.Distance(_navMeshAgent.transform.position, _playerTarget.position);
            float attackRadius = _navMeshAgent.stoppingDistance;

            if (distanceToTarget <= attackRadius)
            {
                Debug.Log("Switching to AttackState");
                stateManager.SwitchState(new AttackState());
            }
            else
            {
                // Если агент застрял или путь отсутствует
                if (!_navMeshAgent.hasPath && !_navMeshAgent.pathPending)
                {
                    Debug.LogWarning("Agent has no path, retrying SetDestination");
                    _navMeshAgent.SetDestination(_playerTarget.position);
                }
            }
        }


        public override void ExitState(StateManager stateManager)
        {
            // Логика выхода из состояния, если нужно
        }
    }

}