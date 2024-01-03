using Unity.VisualScripting;
using UnityEngine;

public class StateMachine<T> : MonoBehaviour, IStateMachine<T> where T : IState
{
    protected T _currentState;

    public virtual void StateTransition(T nextState)
    {
        _currentState?.OnStateEnter();
        _currentState = nextState;
        _currentState?.OnStateExit();
    }

    protected virtual void Update()
    {
        _currentState?.OnStateStay();
    }
}