using UnityEngine;

namespace Top_Down_Shooter.Assets.Scripts.New.StateMachine
{
    public class AttackState : BaseState
    {
       private Transform _playerTarget;
        private EnemyController _enemyController;

        public override void EnterState(StateManager stateManager)
        {
            _enemyController = stateManager.GetEnemyController();
            _playerTarget = _enemyController.GetPlayerTarget();

            Debug.Log("Entered AttackState");
        }

        public override void UpdateState(StateManager stateManager)
        {
            // Проверка расстояния до игрока
            float distanceToTarget = Vector3.Distance(_enemyController.transform.position, _playerTarget.position);
            float attackRadius = _enemyController.GetNavMeshAgent().stoppingDistance;

            if (distanceToTarget > attackRadius)
            {
                Debug.Log("Player moved out of attack range. Switching to MoveState.");
                stateManager.SwitchState(stateManager.move);
            }

            // Выполнение атаки
            PerformAttack();
        }

        public override void ExitState(StateManager stateManager)
        {
            Debug.Log("Exited AttackState");
        }

        private void PerformAttack()
        {
            Debug.Log("Attacking the player...");
            // Здесь можно добавить логику атаки (например, уменьшение здоровья игрока)
        }
    }
}