using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateBase>
{
    public PlayerController Controller { get; private set; }
    public InputManager Input { get; private set; }
    public Transform CameraTransform { get; private set; }

    private enum PlayerState
    {
        Stand,
        Sit,
    }

    Dictionary<PlayerState, PlayerStateBase> _stateDict;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Controller = GetComponent<PlayerController>();
        Input = InputManager.Instance;
        CameraTransform = Camera.main.transform;
        _stateDict = new()
        {
            { PlayerState.Stand, new PlayerStandState(this) },
            { PlayerState.Sit, new PlayerSitState(this) },
        };
        StateTransition(_stateDict[PlayerState.Stand]);
    }
}