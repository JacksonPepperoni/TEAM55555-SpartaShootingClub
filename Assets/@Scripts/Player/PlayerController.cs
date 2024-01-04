using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: Data에서 받아올 것
    [SerializeField] private float moveSpeedBase = 2f;
    [SerializeField] private float moveSpeedModifierSit = 0.5f;
    [SerializeField] private float moveSpeedModifierFastRun = 2f;
    [SerializeField] private float moveSpeedModifierWalk = 0.5f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float sitHeight = 0.5f;
    [SerializeField] private float gravity = Physics.gravity.y;

    private Transform _cinemachineContainer;
    private CharacterController _controller;
    private Vector3 _velocity;
    private InputManager _input;
    private Transform _cameraTransform;
    private Animator _weaponAnimator;

    private bool _isSit;

    private readonly int AnimatorHash_ADSTrigger = Animator.StringToHash("ADSTrigger");
    private readonly int AnimatorHash_MoveVelocity = Animator.StringToHash("MoveVelocity");
    private readonly int AnimatorHash_FastRun = Animator.StringToHash("FastRun");

    public float MoveSpeedValue
    {
        get
        {

            float modifiers = 1f;
            if (_input.Walk)
                modifiers *= moveSpeedModifierWalk;
            else if (_input.FastRun)
                modifiers *= moveSpeedModifierFastRun;

            if (_isSit)
                modifiers *= moveSpeedModifierSit;

            return modifiers * moveSpeedBase;
        }
    }

    public bool IsGround => _controller.isGrounded;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = InputManager.Instance;
        _cameraTransform = Camera.main.transform;
        _weaponAnimator = _cameraTransform.GetComponentInChildren<Animator>();
        _cinemachineContainer = transform.Find("Cinemachine");
    }

    public void Move()
    {
        if (_weaponAnimator == null)
            _weaponAnimator = _cameraTransform.GetComponentInChildren<Animator>();

        if (IsGround && _velocity.y < 0)
            _velocity.y = 0f;

        Vector3 movement = _input.PlayerMovement;
        movement = new Vector3(movement.x, 0f, movement.y);
        movement = _cameraTransform.forward * movement.z + _cameraTransform.right * movement.x;
        movement.y = 0f;
        movement.Normalize();
        _controller.Move(MoveSpeedValue * Time.deltaTime * movement);
        _weaponAnimator.SetFloat(AnimatorHash_MoveVelocity, movement.magnitude);
    }

    public void Sit()
    {
        _cinemachineContainer.localPosition += Vector3.down * 0.5f;
        _isSit = true;
    }

    public void Stand()
    {
        _cinemachineContainer.localPosition += Vector3.up * 0.5f;
        _isSit = false;
    }

    public void ChangeADS()
    {
        if (_weaponAnimator == null)
            _weaponAnimator = _cameraTransform.GetComponentInChildren<Animator>();

        _weaponAnimator.SetTrigger(AnimatorHash_ADSTrigger);
    }

    public void SetFastRun(bool active)
    {
        _weaponAnimator.SetBool(AnimatorHash_FastRun, active);
    }
}