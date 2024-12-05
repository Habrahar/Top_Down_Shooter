public class StateMachine
{
    private IState _currentState;

    public void ChangeState(IState newState)
    {
        _currentState?.Exit(); // Выход из текущего состояния
        _currentState = newState;
        _currentState?.Enter(); // Вход в новое состояние
    }

    public void Update()
    {
        _currentState?.Execute(); // Выполнение текущего состояния
    }
}
