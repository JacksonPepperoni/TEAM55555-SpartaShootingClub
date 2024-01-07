using System.Collections;
using UnityEngine;

public class Weapon_Gun : Weapon
{
    [SerializeField] private WeaponData_Gun _data;

    private Coroutine _shootCoroutine;
    private ParticleSystem _muzzleParticle;
    private Transform _muzzlePoint;

    public WeaponData_Gun Data => _data;

    private void OnEnable() // TODO 플레이어가 무기들때 init 실행
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();

        _muzzlePoint = transform.GetChild(0).Find("MuzzlePoint");

        if (_muzzleParticle == null)
        {
            _muzzleParticle = Instantiate(_data.MuzzleFlash, _muzzlePoint.position, Quaternion.identity, transform.GetChild(0)).GetComponent<ParticleSystem>();
            _muzzleParticle.transform.forward = _muzzlePoint.forward;
        }

        if (_muzzleParticle != null)
            _muzzleParticle?.Stop();

        _shootCoroutine = null;
        _currentAmmo = _data.MagazineCapacity;
        _layerMask = 0b1;
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
                if (!_isFirePress)
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

        //TODO: 반동데이터 받아와서 적용
        CinemachineManager.Instance.ProvideFirearmRecoil(_data);

        lastFireTime = Time.time;
        _currentAmmo--;

        for (int i = 0; i < _data.ShotAtOnce; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            float x = Random.Range(-_data.ShotMOA * 0.5f, _data.ShotMOA * 0.5f);
            float y = Random.Range(-_data.ShotMOA * 0.5f, _data.ShotMOA * 0.5f);
            Vector3 dir = new Vector3(x, y, 0);


            // TODO: 계산식 오류있음.
            if (Physics.Raycast(ray.origin, (ray.direction + dir).normalized, out hit, _data.Range, _layerMask))
            {

                GameObject obj = ResourceManager.Instance.InstantiatePrefab("Metal");
                obj.transform.position = hit.point;
                obj.transform.forward = hit.normal;


                hit.transform.gameObject.GetComponent<HealthSystem>()?.HitDamage(DamageCalculation(hit.point, _data.Damage, _data.Range));
               // Debug.Log(DamageCalculation(hit.point, _data.Damage, _data.Range)); //데미지 계산
            }

            Debug.DrawRay(ray.origin, (ray.direction + dir).normalized * _data.Range, Color.red, 1);
        }
    }


    #endregion

    #region 재장전


    public bool Reload() //BOOL로 만들어서 재장전 성공, 취소 판단할것
    {
        if (_isReloading || _currentAmmo >= _data.MagazineCapacity || _shootCoroutine != null)
            return false;

        _isReloading = true;
        _reloadCoroutine ??= StartCoroutine(ReloadCoroutine());
        return true;
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;

        Debug.Log("리로딩");
        yield return new WaitForSeconds(_data.ReloadTime);

        // 소지탄창수--;
        _currentAmmo = _data.MagazineCapacity;

        Debug.Log("리로드 완료");

        _isReloading = false;
        _reloadCoroutine = null;
    }

    public void ReloadStop() // 장전중에 무기바꾸면 리로딩 취소
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }

        _isReloading = false;
    }

    #endregion
}