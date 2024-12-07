using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Vector3 _playerPos;
    public EnemyChaseState(EnemyController enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }

    public override void EnterState(){ }    
    
    public override void ExitState(){ }
    public override void FrameUpdate(){

        Vector3 _playerPos = PlayerLocator.PlayerTransform.position;

        if (Vector3.Distance(enemy.transform.position, _playerPos) > 0.1f)
        {
            // Двигаемся к текущей точке
            Debug.Log(_playerPos);
            enemy.Follow(_playerPos);
        }
        if(enemy.IsInAttackPosition)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }
}
