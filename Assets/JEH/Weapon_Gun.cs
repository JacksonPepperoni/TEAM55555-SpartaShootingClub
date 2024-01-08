using System.Collections;
using UnityEngine;

public class Weapon_Gun : Weapon
{
    [SerializeField] private WeaponData_Gun _data;

    private Coroutine _shootCoroutine;
    private ParticleSystem _muzzleParticle;
    private Transform _muzzlePoint;
    private AccModifier _accModifier;

    private UISceneTraining _scene;
    private AudioManager _audio;
    private CinemachineManager _cameraManager;
    private Transform _cameraTransform;

    public WeaponData_Gun Data => _data;
    public AccModifier AccModifier => _accModifier;

    private Animator _weaponAnimator;
    private readonly int AnimatorHash_Reload = Animator.StringToHash("Reload");
    private readonly int AnimatorHash_ADSTrigger = Animator.StringToHash("ADSTrigger");


    private void OnEnable() // TODO 플레이어가 무기들때 init 실행
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();

        _weaponAnimator = GetComponent<Animator>();

        _muzzlePoint = transform.GetChild(0).Find("MuzzlePoint");

        if (_muzzleParticle == null)
        {
            _muzzleParticle = Instantiate(_data.MuzzleFlash, _muzzlePoint.position, Quaternion.identity, transform.GetChild(0)).GetComponent<ParticleSystem>();
            _muzzleParticle.transform.forward = _muzzlePoint.forward;
        }

        if (_muzzleParticle != null)
            _muzzleParticle?.Stop();

        _shootCoroutine = null;
        _layerMask = 0b1;

        _scene = UIManager.Instance.SceneUI.GetComponent<UISceneTraining>();
        _audio = AudioManager.Instance;
        _cameraManager = CinemachineManager.Instance;
        _cameraTransform = _cameraManager.WeaponCam.transform;
    }

    public void SetAccessory(AccModifier accMod)
    {
        _accModifier = accMod;

        float aimValue = _accModifier.AimModifier;
        float defaultVal = WeaponEquipManager.Instance.DefaultAdsFOV;
        _cameraManager.ADSFOV = defaultVal * aimValue;

        _currentAmmo = _data.MagazineCapacity + _accModifier.MagazineModifier;
    }

    #region 발사

    public void ShotStart()
    {
        if (_isReloading)
            return;

        if (Time.time < lastFireTime + _data.DelayBetweenShots)
            return;

        _shootCoroutine ??= StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {

            yield return new WaitForSeconds(_data.CastingDelay);

            if (_currentAmmo <= 0)
            {
                CartridgeEmpty();
                _shootCoroutine = null;
                yield break;
            }

            Shoot();

            if (_currentAmmo <= 0 && _data.IsAutoReloading)
            {
                yield return StartCoroutine(ReloadCoroutine());
            }

            yield return new WaitForSeconds(_data.DelayBetweenShots);

            if (!_data.IsContinuousShooting)
            {
                ShootStop();
                yield break;
            }
            else
            {
                if (!InputManager.Instance.FirePress)
                {
                    ShootStop();
                    yield break;
                }
            }
        }
    }

    private void ShootStop()
    {
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
            _shootCoroutine = null;
        }
    }

    protected override void Shoot()
    {
        if (_muzzleParticle != null)
            _muzzleParticle.Play();

        _cameraManager.ProvideFirearmRecoil(_data, _accModifier.RecoilModifier);

        lastFireTime = Time.time;
        _currentAmmo--;

        for (int i = 0; i < _data.ShotAtOnce; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            float x = Random.Range(-_data.ShotMOA * 0.5f, _data.ShotMOA * 0.5f);
            float y = Random.Range(-_data.ShotMOA * 0.5f, _data.ShotMOA * 0.5f);
            Vector3 dir = _cameraTransform.rotation * new Vector3(x, y, 0);

            if (Physics.Raycast(ray.origin, (ray.direction + dir).normalized, out hit, _data.Range, _layerMask))
            {

                GameObject obj = ResourceManager.Instance.InstantiatePrefab("Metal");
                obj.transform.position = hit.point;
                obj.transform.forward = hit.normal;


                hit.transform.gameObject.GetComponent<HealthSystem>()?.HitDamage(DamageCalculation(hit.point, _data.Damage, _data.Range));
                // Debug.Log(DamageCalculation(hit.point, _data.Damage, _data.Range)); //데미지 계산
            }

            //Debug.DrawRay(ray.origin, (ray.direction + dir).normalized * _data.Range, Color.red, 1);

            _scene.UpdateCrosshair(_data.ShotMOA);
            _scene.UpdateMagazine(_currentAmmo); // UI 세팅
            _audio.PlayOneShot(_data.FireSound, _accModifier.SoundModifier); // 발사 사운드
        }
    }


    #endregion

    #region 재장전


    public bool Reload() //BOOL로 만들어서 재장전 성공, 취소 판단할것
    {
        if (_isReloading || _currentAmmo >= _data.MagazineCapacity + _accModifier.MagazineModifier || _shootCoroutine != null)
            return false;

        _isReloading = true;
        _reloadCoroutine ??= StartCoroutine(ReloadCoroutine());
        return true;
    }

    private IEnumerator ReloadCoroutine()
    {
        _weaponAnimator.SetBool(AnimatorHash_Reload, true);

        if (MainScene.Instance.Player.IsADS)
        {
            MainScene.Instance.Player.ChangeADS();
            _weaponAnimator.ResetTrigger(AnimatorHash_ADSTrigger);
        }

        _isReloading = true;

        _audio.PlayOneShot(_data.ReloadSound);
        yield return new WaitForSeconds(_data.ReloadTime);

        _currentAmmo = _data.MagazineCapacity + _accModifier.MagazineModifier; // 소지탄창수--;
        _scene.UpdateMagazine(_currentAmmo); // UI 탄창 업데이트

        _isReloading = false;
        _reloadCoroutine = null;

        _weaponAnimator.SetBool(AnimatorHash_Reload, false);
    }

    public void ReloadStop() // 장전중에 무기바꾸면 리로딩 취소
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }

        _weaponAnimator.SetBool(AnimatorHash_Reload, false);
        _isReloading = false;
    }

    #endregion
}