using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    private InputManager _input;
    private PlayerController _controller;
    private Vector3 _startingRotation;

    // TODO: 마우스 감도 설정값에서 받아올 것
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private float clampAngle = 70f;

    protected override void Awake()
    {
        _input = InputManager.Instance;
        _controller = GetComponentInParent<PlayerController>();

        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (_input == null || vcam.Follow == null)
            return;

        if (stage == CinemachineCore.Stage.Aim)
        {
            if (_startingRotation == null)
                _startingRotation = transform.localRotation.eulerAngles;

            Vector2 deltaInput = _input.MouseDelta;
            _startingRotation.x += deltaInput.x * Time.deltaTime * mouseSensitivity;
            _startingRotation.y += deltaInput.y * Time.deltaTime * mouseSensitivity;
            _startingRotation.y = Mathf.Clamp(_startingRotation.y, -clampAngle, clampAngle);

            state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
        }
    }
}