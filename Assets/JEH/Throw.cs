using System.Collections;
using UnityEngine;

public class Throw : Weapon
{
    [SerializeField] private ThrowDataSO _data;

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
            Reload();
        }

    }


    protected override void Initialize()
    {
        base.Initialize();

        _currentAmmo = _data.Ammo;

        _layerMask = 1 << LayerMask.NameToLayer("Water"); // Water 레이어만 잡힘
    }

    protected override void Shoot()
    {
        if (Time.time < lastFireTime + _data.DelayBetweenShots)
            return;


        if (_currentAmmo <= 0)
            return;


        lastFireTime = Time.time;

        _currentAmmo--;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject rig = Instantiate(_data.Projectile, gameObject.transform.position, Quaternion.identity);
        rig.GetComponent<Rigidbody>()?.AddForce(ray.direction * _data.Power);
    }


    private void Reload() 
    {
        if (_isReloading || _currentAmmo >= _data.Ammo)
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
        _currentAmmo = _data.Ammo;

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
