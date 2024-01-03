using UnityEngine;

public class PlayerStateBase : IState
{
    protected PlayerController _controller;
    protected InputManager _input;
    protected Transform _cameraTransform;

    public PlayerStateBase(PlayerStateMachine machine)
    {
        _controller = machine.Controller;
        _input = machine.Input;
        _cameraTransform = machine.CameraTransform;
    }

    public virtual void OnStateEnter()
    {
    }

    public virtual void OnStateExit()
    {
    }

    public virtual void OnStateStay()
    {
    }
}

public class PlayerGroundState : PlayerStateBase
{
    public PlayerGroundState(PlayerStateMachine machine) : base(machine) { }

    public override void OnStateEnter()
    {
    }

    public override void OnStateStay()
    {
        _controller.Move();
    }

    public override void OnStateExit()
    {
    }
}

public class PlayerStandState : PlayerGroundState
{
    public PlayerStandState(PlayerStateMachine machine) : base(machine) { }
}

public class PlayerSitState : PlayerGroundState
{
    public PlayerSitState(PlayerStateMachine machine) : base(machine) { }
}