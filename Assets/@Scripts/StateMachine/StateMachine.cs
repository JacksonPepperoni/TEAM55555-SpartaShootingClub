using UnityEngine;

public class StateMachine<T> : MonoBehaviour, IStateMachine<T> where T : IState
{
    protected T _currentState;

    public virtual void StateTransition(T nextState)
    {
        if (nextState.Equals(_currentState))
            return;

        _currentState?.OnStateExit();
        _currentState = nextState;
        _currentState?.OnStateEnter();
    }

    protected virtual void Update()
    {
        _currentState?.OnStateStay();
    }
}