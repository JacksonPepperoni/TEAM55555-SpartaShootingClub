using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GunDataSO _gunData;

    private Coroutine _shootCoroutine;

    public GameObject TestPrefab;


    private void OnEnable() // TODO 플레이어가 init 실행
    {
        Initialize();
    }


    protected override void Initialize()
    {
        transform.localPosition = Vector3.zero;

        lastFireTime = 0 - _gunData.DelayBetweenShots;
        _currentAmmo = _gunData.Ammo;

        _isReloading = false;
        _isLeftPress = false;
        _shootCoroutine = null;
        _reloadCoroutine = null;

        _layerMask = 1 << LayerMask.NameToLayer("Water"); // Water 레이어만 잡힘

    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            _isLeftPress = true;
            ShotStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isLeftPress = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadStart();
        }


    }


    #region 발사

    private void ShotStart()
    {
        if (_isReloading)
            return;

        if (Time.time < lastFireTime + _gunData.DelayBetweenShots)
            return;



        _shootCoroutine ??= StartCoroutine(ShootCoroutine());

    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (_currentAmmo <= 0)
            {
                CartridgeEmpty();
                yield break;
            }

            Shoot();

            if (_currentAmmo <= 0 && _gunData.IsAutoReloading)
            {
                yield return StartCoroutine(ReloadCoroutine());
            }

            yield return new WaitForSeconds(_gunData.DelayBetweenShots);

            if (!_gunData.IsContinuousShooting)
            {
                ShootStop();
                yield break;
            }
            else
            {
                if (!_isLeftPress)
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
        lastFireTime = Time.time;
        _currentAmmo--;

        for (int i = 0; i < _gunData.NumberOfBulletFiredAtOnce; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            float x = Random.Range(-_gunData.Spread, _gunData.Spread);
            float y = Random.Range(-_gunData.Spread, _gunData.Spread);
            Vector3 dir = new Vector3(x, y, 0);

            if (Physics.Raycast(ray.origin, (ray.direction + dir).normalized, out hit, _gunData.RangeOfHits, _layerMask))
            {

                Instantiate(TestPrefab, hit.point, Quaternion.LookRotation(hit.normal)); // 탄흔

                if (hit.collider.CompareTag("Wall"))
                {
                    //  Debug.Log("벽");
                }
            }

            Debug.DrawRay(ray.origin, (ray.direction + dir).normalized * _gunData.RangeOfHits, Color.red, 1);

        }

    }


    #endregion

    #region 재장전


    private void ReloadStart() //BOOL로 만들어서 재장전 성공, 취소 판단할것
    {
        if (_isReloading || _currentAmmo >= _gunData.Ammo)
            return;

        _isReloading = true;
        _reloadCoroutine ??= StartCoroutine(ReloadCoroutine());

    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;

        Debug.Log("리로딩");
        yield return new WaitForSeconds(_gunData.ReloadTime);

        // 소지탄창수--;
        _currentAmmo = _gunData.Ammo;

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

}


