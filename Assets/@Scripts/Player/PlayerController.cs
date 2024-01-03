using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: Data에서 받아올 것
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float gravity = Physics.gravity.y;

    private CharacterController _controller;
    private Vector3 _velocity;
    private InputManager _input;
    private Transform _cameraTransform;

    public bool IsGround => _controller.isGrounded;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = InputManager.Instance;
        _cameraTransform = Camera.main.transform;
    }

    public void Move()
    {

        if (IsGround && _velocity.y < 0)
            _velocity.y = 0f;

        Vector3 movement = _input.PlayerMovement;
        movement = new Vector3(movement.x, 0f, movement.y);
        movement = transform.forward * movement.z + transform.right * movement.x;
        movement.y = 0f;
        _controller.Move(playerSpeed * Time.deltaTime * movement);
    }

    public void LookForward(Vector3 forward)
    {
        transform.forward = forward;
    }
}