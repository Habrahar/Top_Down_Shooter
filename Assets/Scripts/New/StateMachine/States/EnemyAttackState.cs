using System.Collections;
using System.Collections.Generic;
using New;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }

    public override void EnterState()
    {
        if (enemy.Target != null)
        {
            enemy.Target.TakeDamage(100);
            Debug.Log("Нанесен урон игроку");
        }
        else
        {
            Debug.LogError("Цель не задана!");
        }
    }
    public override void ExitState(){ }

    public override void FrameUpdate()
    {
        
    }
}
