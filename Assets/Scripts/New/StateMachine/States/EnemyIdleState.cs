using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 _targetpos;
    private Vector3 _direction;
    public EnemyIdleState(EnemyController enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState(){ 
        _targetpos = GetRandomPointInCircle();
    }
    public override void ExitState(){ }
    public override void FrameUpdate(){ 
        // Проверяем расстояние до цели
        if (Vector3.Distance(enemy.transform.position, _targetpos) > 0.1f)
        {
            // Двигаемся к текущей точке
            enemy.Follow(_targetpos);
        }
        else
        {
            // Генерируем новую случайную точку, если цель достигнута
            _targetpos = GetRandomPointInCircle();
        }
        
    }

    private Vector3 GetRandomPointInCircle()
    {
        // Генерируем случайную точку в радиусе RandomMovemnt вокруг текущей позиции врага
        Vector2 randomPoint2D = UnityEngine.Random.insideUnitCircle * enemy.RandomMovemnt;

        // Преобразуем в X-Z плоскость
        Vector3 randomPoint = new Vector3(
            enemy.transform.position.x + randomPoint2D.x, // Случайная точка по X
            enemy.transform.position.y,                  // Высота остаётся неизменной
            enemy.transform.position.z + randomPoint2D.y // Случайная точка по Z
        );

        return randomPoint;
    }

}
    
