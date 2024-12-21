using System.Collections;
using System.Collections.Generic;
using New;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 _targetpos;
    public EnemyIdleState(EnemyController enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState(){ 
        
        _targetpos = GetRandomPointInCircle();
    }
    public override void ExitState(){ }
    public override void FrameUpdate(){
        if (Vector3.Distance(enemy.transform.position, _targetpos) > 0.1f)
        {
            enemy.Follow(_targetpos);
        }
        else
        {
            _targetpos = GetRandomPointInCircle();
        }
        
    }

    private Vector3 GetRandomPointInCircle()
    {
        Vector2 randomPoint2D = UnityEngine.Random.insideUnitCircle * enemy.RandomMovemnt;
        Vector3 randomPoint = new Vector3(
            enemy.transform.position.x + randomPoint2D.x,
            enemy.transform.position.y,
            enemy.transform.position.z + randomPoint2D.y
        );
        return randomPoint;
    }

}
    
