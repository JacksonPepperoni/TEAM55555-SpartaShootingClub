using System.Collections;
using UnityEngine;


public class Weapon_Gun : Weapon
{
    [SerializeField] private WeaponData_Gun _data;

    private Coroutine _shootCoroutine;
    private ParticleSystem _muzzleParticle;
    private Transform _muzzlePoint;

    private void OnEnable() // TODO 플레이어가 무기들때 init 실행
    {
        // Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();

        _muzzlePoint = transform.GetChild(0).Find("MuzzlePoint");

        if (_muzzleParticle == null)
        {
            _muzzleParticle = Instantiate(_data.MuzzleFlash, _muzzlePoint.position, Quaternion.identity, transform.GetChild(0)).GetComponent<ParticleSystem>();
            _muzzleParticle.transform.forward = _muzzlePoint.forward *= -1f; // 테스트총이 반대방향이라 반대로 해놨습니다
        }

        if (_muzzleParticle != null)
            _muzzleParticle?.Stop();

        _shootCoroutine = null;
        _currentAmmo = _data.MagazineCapacity;
        _layerMask = 0b1;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isFirePress = true;
            ShotStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isFirePress = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }


    #region 발사

    private void ShotStart()
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


        lastFireTime = Time.time;
        _currentAmmo--;

        for (int i = 0; i < _data.ShotAtOnce; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            float x = Random.Range(-_data.Spread * 0.5f, _data.Spread * 0.5f);
            float y = Random.Range(-_data.Spread * 0.5f, _data.Spread * 0.5f);
            Vector3 dir = new Vector3(x, y, 0);

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


    private void Reload() //BOOL로 만들어서 재장전 성공, 취소 판단할것
    {
        if (_isReloading || _currentAmmo >= _data.MagazineCapacity || _shootCoroutine != null)
            return;

        _isReloading = true;
        _reloadCoroutine ??= StartCoroutine(ReloadCoroutine());

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

    private void ReloadStop() // 장전중에 무기바꾸면 리로딩 취소
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }

        _isReloading = false;
    }

    #endregion

    public void GetWeaponData(WeaponData_Gun weaponData_Gun)
    {
        _data = weaponData_Gun;
        Initialize();
    }

}


