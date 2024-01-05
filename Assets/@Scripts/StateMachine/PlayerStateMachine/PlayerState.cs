using UnityEngine;

public class PlayerStateBase : IState
{
    protected PlayerStateMachine _machine;
    protected PlayerController _controller;
    protected InputManager _input;
    protected Transform _cameraTransform;

    public PlayerStateBase(PlayerStateMachine machine)
    {
        _machine = machine;
        _controller = machine.Controller;
        _input = machine.Input;
        _cameraTransform = machine.CameraTransform;
    }

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }

    public virtual void OnStateStay() { }
}

public class PlayerGroundState : PlayerStateBase
{
    public PlayerGroundState(PlayerStateMachine machine) : base(machine) { }

    public override void OnStateStay()
    {
        _controller.Move();
        if (_input.ADSTrigger)
            _controller.ChangeADS();
        _controller.SetFastRun(_input.FastRunPress);
        if (!_input.FastRunPress && _input.FirePress)
        {
            Debug.Log("fire");
        }
    }
}

public class PlayerStandState : PlayerGroundState
{
    public PlayerStandState(PlayerStateMachine machine) : base(machine) { }

    public override void OnStateEnter()
    {
        _controller.Sit(false);
    }

    public override void OnStateStay()
    {
        base.OnStateStay();
        if (_input.SitKeyDown)
            _machine.StateTransition(_machine.StateDictionary[PlayerStateMachine.PlayerState.Sit]);
    }
}

public class PlayerSitState : PlayerGroundState
{
    public PlayerSitState(PlayerStateMachine machine) : base(machine) { }

    public override void OnStateEnter()
    {
        _controller.Sit(true);
    }

    public override void OnStateStay()
    {
        base.OnStateStay();
        if (_input.SitKeyDown)
            _machine.StateTransition(_machine.StateDictionary[PlayerStateMachine.PlayerState.Stand]);
    }
}