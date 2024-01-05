using Cinemachine;
using System.Collections;
using UnityEngine;

public class CinemachineManager : Singleton<CinemachineManager>
{
    private CinemachineVirtualCamera _vcam;
    private Camera _weaponCam;
    private CinemachinePOVExtension _povExtension;
    private PlayerController _player;

    private float _defaultFOV;
    private float _adsFOV = 70f;
    private Coroutine _coADSChange;

    public float DefaultFOV
    {
        get => _defaultFOV;
        set
        {
            _defaultFOV = value;
            if (!_player.IsADS)
            {
                _vcam.m_Lens.FieldOfView = value;
                _weaponCam.fieldOfView = value;
            }
        }
    }

    public float ADSFOV
    {
        get => _adsFOV;
        set
        {
            _adsFOV = value;
            if (_player.IsADS)
            {
                _vcam.m_Lens.FieldOfView = value;
                _weaponCam.fieldOfView = value;
            }
        }
    }

    public CinemachineVirtualCamera Vcam
    {
        get
        {
            if (_vcam == null)
                _vcam = GameObject.FindWithTag("Player").GetComponentInChildren<CinemachineVirtualCamera>();
            return _vcam;
        }
    }

    public Camera WeaponCam
    {
        get
        {
            if (_weaponCam == null)
                _weaponCam = Camera.main.transform.GetChild(0).GetComponent<Camera>();
            return _weaponCam;
        }
    }

    public CinemachinePOVExtension PovExtension
    {
        get
        {
            if (_povExtension == null) 
                _povExtension = Vcam.GetComponent<CinemachinePOVExtension>();
            return _povExtension;
        }
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        return true;
    }

    public void ADSFOVChange(bool adsActive, float duration = .1f, bool overrideCoroutine = true)
    {
        float toFOV = adsActive ? _adsFOV : _defaultFOV;
        if (overrideCoroutine)
        {
            if (_coADSChange != null)
                StopCoroutine(_coADSChange);
            _coADSChange = StartCoroutine(CoADSChange(_vcam.m_Lens.FieldOfView, toFOV, duration));
        }
        else
        {
            if (_coADSChange == null)
                _coADSChange = StartCoroutine(CoADSChange(_vcam.m_Lens.FieldOfView, toFOV, duration));
        }
    }

    private IEnumerator CoADSChange(float fromFOV, float toFOV, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float ratio = Mathf.Clamp01(t / duration);
            float newFOV = Mathf.Lerp(fromFOV, toFOV, ratio);
            _vcam.m_Lens.FieldOfView = newFOV;
            _weaponCam.fieldOfView = newFOV;
            yield return null;
        }
        _vcam.m_Lens.FieldOfView = toFOV;
        _weaponCam.fieldOfView = toFOV;
        _coADSChange = null;
    }

    public void ProvideFirearmRecoil(float verticalForce, float horizontalForce, float time) => PovExtension.ReceiveFirearmRecoil(verticalForce, horizontalForce, time);
}