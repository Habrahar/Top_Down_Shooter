using UnityEngine;
public class AttackState : IState
{
    public void Enter()
    {
        Debug.Log("Entering AttackState");
    }

    public void Execute()
    {
        Debug.Log("Attacking the player");
    }

    public void Exit()
    {
        Debug.Log("Exiting AttackState");
    }
}
