using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: Data에서 받아올 것
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float gravity = Physics.gravity.y;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGround => _controller.isGrounded;
    private InputManager _input;
    private Transform _cameraTransform;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = InputManager.Instance;
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (_isGround && _velocity.y < 0)
            _velocity.y = 0f;

        Vector3 movement = _input.PlayerMovement;
        movement = new Vector3(movement.x, 0f, movement.y);
        movement = _cameraTransform.forward * movement.z + _cameraTransform.right * movement.x;
        movement.y = 0f;
        _controller.Move(playerSpeed * Time.deltaTime * movement.normalized);
    }

    private void LateUpdate()
    {
        transform.forward = _cameraTransform.forward;
    }
}