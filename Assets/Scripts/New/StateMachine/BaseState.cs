using UnityEngine;

namespace Top_Down_Shooter.Assets.Scripts.New.StateMachine
{
    public abstract class BaseState
    {
        public abstract void EnterState(StateManager stateManager);
        public abstract void UpdateState(StateManager stateManager);
        public abstract void ExitState(StateManager stateManager);

    }
}