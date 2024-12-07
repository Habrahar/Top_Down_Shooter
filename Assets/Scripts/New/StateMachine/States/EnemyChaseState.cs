using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }

    public override void EnterState(){ }    
    
    public override void ExitState(){ }
    public override void FrameUpdate(){
        if(enemy.IsAggred)
        {
            if (Vector3.Distance(enemy.transform.position, PlayerLocator.PlayerTransform.position) > 0.1f)
            {
                enemy.Follow(PlayerLocator.PlayerTransform.position);
            }
            if(enemy.IsInAttackPosition)
            {
                enemy.StateMachine.ChangeState(enemy.AttackState);
            }
        }else
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
        
    }
}
