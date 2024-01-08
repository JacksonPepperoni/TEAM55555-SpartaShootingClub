using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: Data에서 받아올 것
    [SerializeField] private float moveSpeedBase = 2f;
    [SerializeField] private float moveSpeedModifierSit = 0.5f;
    [SerializeField] private float moveSpeedModifierFastRun = 2f;
    [SerializeField] private float moveSpeedModifierWalk = 0.5f;
    [SerializeField] private float sitHeight = 0.5f;
    [SerializeField] private float sitStandDuration = 0.1f;
    [SerializeField] private Vector3 defaultHeight = new Vector3(0, 1.5f, 0);
    //[SerializeField] private float jumpHeight = 1f;
    //[SerializeField] private float gravity = Physics.gravity.y;

    private Transform _cinemachineContainer;
    private CharacterController _controller;
    private Vector3 _velocity;
    private InputManager _input;
    private Transform _cameraTransform;
    private Animator _weaponAnimator;
    private AudioManager _audio;
    private UIManager _ui;

    private bool _isSit;
    private bool _isADS;
    private Coroutine _coSitAndStandHeightChange;

    private readonly int AnimatorHash_ADSTrigger = Animator.StringToHash("ADSTrigger");
    private readonly int AnimatorHash_MoveVelocity = Animator.StringToHash("MoveVelocity");
    private readonly int AnimatorHash_FastRun = Animator.StringToHash("FastRun");

    // TEST CODE
    // TODO: 총기반동 Data에서 받아올 것
    public float verticalRecoil = 10f;
    public float horizontalRecoil = 10f;
    public float recoilDuration = 0.1f;

    public bool IsADS => _isADS;
    public bool IsSit => _isSit;
    public bool IsMove => _velocity.magnitude > 0.01f;
    public bool IsWalk => IsMove && _input.WalkPress;
    public bool IsRun => IsMove && !_input.WalkPress && !_input.FastRunPress;
    public bool IsFastRun => IsMove && _input.FastRunPress;


    public float MoveSpeedValue
    {
        get
        {
            float modifiers = 1f;
            if (_input.WalkPress)
                modifiers *= moveSpeedModifierWalk;
            else if (_input.FastRunPress)
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
        _audio = AudioManager.Instance;
        _ui = UIManager.Instance; 
    }

    public void Move()
    {
        if (_weaponAnimator == null)
            _weaponAnimator = _cameraTransform.GetComponentInChildren<Animator>();

        if (IsGround && _velocity.y < 0)
            _velocity.y = 0f;

        _velocity = _input.PlayerMovement;
        _velocity = new Vector3(_velocity.x, 0f, _velocity.y);
        _velocity = _cameraTransform.forward * _velocity.z + _cameraTransform.right * _velocity.x;
        _velocity.y = 0f;
        _velocity.Normalize();
        _controller.Move(MoveSpeedValue * Time.deltaTime * _velocity);
        _weaponAnimator.SetFloat(AnimatorHash_MoveVelocity, _velocity.magnitude);

        // 플레이어 움직임 사운드
        if (_velocity.sqrMagnitude > 0f)
        {
            _audio.MovementSound();
        }
        else if (_audio.Source.isPlaying)
        {
            _audio.Source.Pause();
        }
    }

    public void Sit(bool active)
    {
        if (_coSitAndStandHeightChange != null)
            StopCoroutine(_coSitAndStandHeightChange);

        var fromHeigh = _cinemachineContainer.localPosition;
        var toHeight = active ? defaultHeight + Vector3.down * sitHeight : defaultHeight + Vector3.up * sitHeight;

        _coSitAndStandHeightChange = StartCoroutine(CoSitAndStandHeightChange(fromHeigh, toHeight, sitStandDuration));

        _isSit = active;
    }

    public void ChangeADS()
    {
        if (_weaponAnimator == null)
            _weaponAnimator = _cameraTransform.GetComponentInChildren<Animator>();

        _isADS = !_isADS;
        _weaponAnimator.SetTrigger(AnimatorHash_ADSTrigger);

        //TODO: 총기 줌 속도와 동일한 duration 제공
        CinemachineManager.Instance.ADSFOVChange(_isADS, 0.1f);

        if (!_isADS) _ui.SceneUI.GetComponent<UISceneTraining>().ShowCrossHair();
        else _ui.SceneUI.GetComponent<UISceneTraining>().HideCrossHair();
    }

    public void SetFastRun(bool active)
    {
        _weaponAnimator.SetBool(AnimatorHash_FastRun, active);
        
        if (active)
        {
            if (IsADS)
                ChangeADS();
            _ui.SceneUI.GetComponent<UISceneTraining>().UpdateIdle(1); // UI 스탠딩 이미지 변경
            _ui.SceneUI.GetComponent<UISceneTraining>().ShowCrossHair();
        }
        else
        {
            if (_isSit) _ui.SceneUI.GetComponent<UISceneTraining>().UpdateIdle(2);
            else _ui.SceneUI.GetComponent<UISceneTraining>().UpdateIdle(0);
        }
    }

    private IEnumerator CoSitAndStandHeightChange(Vector3 fromHeight, Vector3 toHeight, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float ratio = Mathf.Clamp01(t / duration);
            Vector3 newHeight = Vector3.Lerp(fromHeight, toHeight, ratio);
            _cinemachineContainer.localPosition = newHeight;
            yield return null;
        }
        _cinemachineContainer.localPosition = toHeight;
        _coSitAndStandHeightChange = null;
    }
}