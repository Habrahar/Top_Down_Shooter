using UnityEngine;

public class MoveToTargetState : IState
{
    private EnemyController _enemy;
    private Transform _target;

    public MoveToTargetState(EnemyController enemy, Transform target)
    {
        _enemy = enemy;
        _target = target;
    }

    public void Enter()
    {
        Debug.Log("Entering MoveToTargetState");
    }

    public void Execute()
    {
        if (_target != null)
        {
            _enemy.MoveTo(_target.position);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting MoveToTargetState");
    }
}
