using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction move;
    [SerializeField] private InputAction aim;

    public Vector2 MouseDelta => aim.ReadValue<Vector2>();
    public Vector2 PlayerMovement => move.ReadValue<Vector2>();

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        playerInput = gameObject.AddComponent<PlayerInput>();
        playerInput.actions = ResourceManager.Instance.GetCache<InputActionAsset>("PlayerInputActions");
        playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        playerInput.SwitchCurrentActionMap("Player");
        move = playerInput.currentActionMap.FindAction("Move");
        aim = playerInput.currentActionMap.FindAction("Aim");
        Cursor.visible = false;

        return true;
    }
}