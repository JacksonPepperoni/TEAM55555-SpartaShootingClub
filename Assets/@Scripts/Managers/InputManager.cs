using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction move;
    [SerializeField] private InputAction aim;
    [SerializeField] private InputAction fire;
    [SerializeField] private InputAction ads;
    [SerializeField] private InputAction sit;

    public Vector2 MouseDelta => aim.ReadValue<Vector2>();
    public Vector2 PlayerMovement => move.ReadValue<Vector2>();

    public bool FireTrigger => fire.triggered;
    public bool ADSTrigger => ads.triggered;
    public bool SitKeyDown => sit.triggered;
    public bool SitKeyUp => !sit.inProgress;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;
        playerInput = gameObject.AddComponent<PlayerInput>();
        playerInput.actions = ResourceManager.Instance.GetCache<InputActionAsset>("PlayerInputActions");
        playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        playerInput.SwitchCurrentActionMap("Player");
        move = playerInput.currentActionMap.FindAction("Move");
        aim = playerInput.currentActionMap.FindAction("Aim");
        fire = playerInput.currentActionMap.FindAction("Fire");
        ads = playerInput.currentActionMap.FindAction("ADS");
        sit = playerInput.currentActionMap.FindAction("Sit");
        Cursor.visible = false;

        return true;
    }
}