using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private PlayerController player; // Ссылка на игрока

    public EnemyAttackState(EnemyController enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        if (PlayerLocator.PlayerTransform != null)
        {
            player = PlayerLocator.PlayerTransform.GetComponent<PlayerController>();
        }
    }

    public override void EnterState()
    {
        
        if (player != null && enemy.IsInAttackPosition)
        {
            int damage = 10; // Урон, наносимый игроку
            player.TakeDamage(damage);
        }

    }
    public override void ExitState(){ }

    public override void FrameUpdate()
    {
        if(enemy.IsInAttackPosition)
        {
            
        }else
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }
}
