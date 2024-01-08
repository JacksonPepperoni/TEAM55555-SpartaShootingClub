using Cinemachine;
using System.Collections;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    private InputManager _input;
    private Vector3 _startingRotation;
    private Vector3 _recoilVelocity;
    private Coroutine _coRecoilVelocityCalculate;
    private float _mouseSensitivity = 10f;
    private float _clampAngle = 70f;
    private bool _mouseInversion = false;

    public float MouseSensitivity { get => _mouseSensitivity; set => _mouseSensitivity = value; }
    public bool MouseInversion { get => _mouseInversion; set => _mouseInversion = value; }

    protected override void Awake()
    {
        _input = InputManager.Instance;

        base.Awake();
    }

    // 총기 반동 적용
    public void ReceiveFirearmRecoil(WeaponData_Gun data, float recoilModifier)
    {
        if (_coRecoilVelocityCalculate != null)
            StopCoroutine(_coRecoilVelocityCalculate);

        _coRecoilVelocityCalculate = StartCoroutine(CoRecoilVelocityCalculate(data, recoilModifier));
    }

    IEnumerator CoRecoilVelocityCalculate(WeaponData_Gun data , float recoilModifier)
    {
        for (float t = 0f; t < data.RecoilDuration; t += Time.deltaTime)
        {
            _recoilVelocity = data.RecoilCurve.Evaluate(t / data.RecoilDuration) * new Vector3(data.HorizontalRecoilForce * recoilModifier, data.VerticalRecoilForce * recoilModifier);
            yield return null;
        }
        _recoilVelocity = Vector3.zero;
        _coRecoilVelocityCalculate = null;
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
            _startingRotation.x += deltaInput.x * Time.deltaTime * _mouseSensitivity;
            _startingRotation.y += deltaInput.y * Time.deltaTime * _mouseSensitivity;
            _startingRotation.y = Mathf.Clamp(_startingRotation.y, -_clampAngle, _clampAngle);
            _startingRotation.y *= _mouseInversion ? -1f : 1f;

            _startingRotation += _recoilVelocity;

            state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
        }
    }
}