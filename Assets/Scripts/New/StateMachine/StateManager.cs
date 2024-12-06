using UnityEngine;

namespace Top_Down_Shooter.Assets.Scripts.New.StateMachine
{
    public class StateManager : MonoBehaviour
    {
        private BaseState _currentState;
        [SerializeField] private EnemyController _enemy; // Ссылка на EnemyController

        private void Start()
        {
            // Устанавливаем начальное состояние при старте
            _currentState = new MoveState();
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState?.UpdateState(this);
        }

        public void SwitchState(BaseState newState)
        {
            _currentState?.ExitState(this);
            _currentState = newState;
            _currentState.EnterState(this);
        }

        public EnemyController GetEnemyController()
        {
            return _enemy;
        }
    }
}