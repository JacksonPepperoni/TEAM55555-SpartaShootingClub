using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class SettingsManager : Singleton<SettingsManager>
{
    private CinemachineVirtualCamera _vcam;
    private Camera _weaponCam;
    private CinemachinePOVExtension _povExtension;

    // Settings Value
    private float _fov;
    private float _sensitivity;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        // TODO: JSON이나 PlayerPrefs에서 저장된 값 불러오기
        _fov = PlayerPrefs.GetFloat("Settings_Fov", 90);
        _sensitivity = PlayerPrefs.GetFloat("Settings_Sensitivity", 50);

        SetFOV(_fov);
        SetMouseSensitivity(_sensitivity);

        return true;
    }

    public float FOV
    {
        get => _fov;
        set => SetFOV(value);
    }

    public float MouseSensitivity
    {
        get => _sensitivity;
        set => SetMouseSensitivity(value);
    }

    public bool SetFOV(float fov)
    {
        if (_vcam == null)
        {
            _vcam = GameObject.FindWithTag("Player").GetComponentInChildren<CinemachineVirtualCamera>();
            if (_vcam == null)
                return false;
        }

        if (_weaponCam == null)
        {
            _weaponCam = Camera.main.transform.GetChild(0).GetComponent<Camera>();
            if (_weaponCam == null)
                return false;
        }

        _fov = fov;
        _vcam.m_Lens.FieldOfView = fov;
        _weaponCam.fieldOfView = fov;

        // 임시 PlayerPrefs 저장
        PlayerPrefs.SetFloat("Settings_Fov", fov);

        return true;
    }

    public bool SetMouseSensitivity(float sensitivity)
    {
        if (_povExtension == null)
        {
            _povExtension = GameObject.FindWithTag("Player").GetComponentInChildren<CinemachinePOVExtension>();
            if (_povExtension == null)
                return false;
        }

        _sensitivity = sensitivity;
        _povExtension.MouseSensitivity = sensitivity;

        // 임시 PlayerPrefs 저장
        PlayerPrefs.SetFloat("Settings_Sensitivity", sensitivity);

        return true;
    }
}