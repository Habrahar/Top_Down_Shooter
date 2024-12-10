using System.Collections;
using System.Collections.Generic;
using New;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }

    public override void EnterState(){ }

    public override void ExitState()
    {
        
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public override void FrameUpdate(){
        if (enemy.Target != null)
        {
            Vector3 targetPosition = enemy.GetTargetPosition();
            enemy.Follow(targetPosition);
        }
        else
        {
            Debug.LogError("Цель не задана!");
        }
    }
}
