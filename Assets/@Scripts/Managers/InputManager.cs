using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _aimAction;
    private InputAction _fireAction;
    private InputAction _adsAction;
    private InputAction _sitAction;
    private InputAction _fastRunAction;
    private InputAction _walkAction;

    private bool _fastRun;
    private bool _walk;

    public Vector2 MouseDelta => _aimAction.ReadValue<Vector2>();
    public Vector2 PlayerMovement => _moveAction.ReadValue<Vector2>();

    public bool FireTrigger => _fireAction.triggered;
    public bool ADSTrigger => _adsAction.triggered;
    public bool SitKeyDown => _sitAction.triggered;
    //public bool SitKeyUp => !sit.inProgress;
    public bool FastRun => _fastRun;
    public bool Walk => _walk;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        _playerInput = gameObject.AddComponent<PlayerInput>();
        _playerInput.actions = ResourceManager.Instance.GetCache<InputActionAsset>("PlayerInputActions");
        _playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        _playerInput.SwitchCurrentActionMap("Player");

        _moveAction = _playerInput.currentActionMap.FindAction("Move");
        _aimAction = _playerInput.currentActionMap.FindAction("Aim");
        _fireAction = _playerInput.currentActionMap.FindAction("Fire");
        _adsAction = _playerInput.currentActionMap.FindAction("ADS");
        _sitAction = _playerInput.currentActionMap.FindAction("Sit");
        _fastRunAction = _playerInput.currentActionMap.FindAction("FastRun");
        _walkAction = _playerInput.currentActionMap.FindAction("Walk");

        // 키 상호작용
        // ex) 달리다가 정조준하면 달리기 취소. 걷다가 달리면 걷기 취소
        _adsAction.started += _ => _fastRun = false;
        _fastRunAction.started += _ => { _fastRun = true; _walk = false; };
        _fastRunAction.canceled += _ => _fastRun = false;
        _walkAction.started += _ => { _walk = true; _fastRun = false; };
        _walkAction.canceled += _ => _walk = false;

        Cursor.visible = false;

        return true;
    }
}