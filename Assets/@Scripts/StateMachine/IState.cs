public interface IState
{
    void OnStateEnter();
    void OnStateStay();
    void OnStateExit();
}

public interface IStateMachine<T> where T : IState
{
    void StateTransition(T nextState);
}