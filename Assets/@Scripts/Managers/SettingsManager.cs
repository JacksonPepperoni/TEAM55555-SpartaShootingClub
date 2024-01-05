using Cinemachine;
using UnityEngine;

public class SettingsManager : Singleton<SettingsManager>
{
    private CinemachineManager _camManager;

    // Settings Control Value
    private bool _mouseReverse;
    private float _fov;
    private float _sensitivity;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        _camManager = CinemachineManager.Instance;

        // TODO: JSON이나 PlayerPrefs에서 저장된 값 불러오기

        _mouseReverse = PlayerPrefs.GetInt("Settings_Inversion", 0) == 1;
        _fov = PlayerPrefs.GetFloat("Settings_Fov", 90);
        _sensitivity = PlayerPrefs.GetFloat("Settings_Sensitivity", 50);

        SetFOV(_fov);
        SetMouseSensitivity(_sensitivity);

        return true;
    }

    #region Control Settings

    public bool MouseReverse 
    { 
        get => _mouseReverse; 
        set => SetReverse(value); 
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

    public bool SetReverse(bool reverse)
    {
        if (_camManager.PovExtension == null)
            return false;

        _mouseReverse = reverse;
        _camManager.PovExtension.MouseInversion = reverse;

        // 임시 PlayerPrefs 저장
        PlayerPrefs.SetInt("Settings_Inversion", _mouseReverse ? 1 : 0);

        return true;
    }

    public bool SetFOV(float fov)
    {
        if (_camManager.Vcam == null || _camManager.WeaponCam == null)
            return false;

        _fov = fov;
        _camManager.DefaultFOV = fov;

        // 임시 PlayerPrefs 저장
        PlayerPrefs.SetFloat("Settings_Fov", fov);

        return true;
    }

    public bool SetMouseSensitivity(float sensitivity)
    {
        if (_camManager.PovExtension == null)
            return false;

        _sensitivity = sensitivity;
        _camManager.PovExtension.MouseSensitivity = sensitivity;

        // 임시 PlayerPrefs 저장
        PlayerPrefs.SetFloat("Settings_Sensitivity", sensitivity);

        return true;
    }

    #endregion
}