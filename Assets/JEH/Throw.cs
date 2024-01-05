using System.Collections;
using UnityEngine;

public class Throw : Weapon
{
    [SerializeField] private ThrowDataSO _throwData;


    private void OnEnable() // TODO 플레이어가 init 실행
    {
        Initialize();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isLeftPress = true;
            Shoot();
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


    protected override void Initialize()
    {
        transform.localPosition = Vector3.zero;
        _currentAmmo = _throwData.Ammo;

        lastFireTime = 0;

        _isReloading = false;
        _isLeftPress = false;

        _layerMask = 1 << LayerMask.NameToLayer("Water"); // Water 레이어만 잡힘
    }

    protected override void Shoot()
    {
        if (Time.time < lastFireTime + _throwData.DelayBetweenShots)
            return;


        if (_currentAmmo <= 0)
            return;


        lastFireTime = Time.time;

        _currentAmmo--;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject rig = Instantiate(_throwData.Projectile, gameObject.transform.position, Quaternion.identity);
        rig.GetComponent<Rigidbody>()?.AddForce(ray.direction * _throwData.Power);
    }


    private void ReloadStart() //BOOL로 만들어서 재장전 성공, 취소 판단할것
    {
        if (_isReloading || _currentAmmo >= _throwData.Ammo)
            return;

        _isReloading = true;
        _reloadCoroutine ??= StartCoroutine(ReloadCoroutine());

    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;

        Debug.Log("리로딩");
        yield return new WaitForSeconds(_throwData.ReloadTime);

        // 소지탄창수--;
        _currentAmmo = _throwData.Ammo;

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

}
