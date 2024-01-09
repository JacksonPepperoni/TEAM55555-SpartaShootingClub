using System.Collections;
using UnityEngine;

public class Weapon_Throw : Weapon
{
    [SerializeField] private WeaponData_Throw _data;

    private void OnEnable() // TODO 플레이어가 무기들때 init 실행
    {
        Initialize();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    _isFirePress = true;
        //    Shoot();
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    _isFirePress = false;
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Reload();
        //}
    }


    protected override void Initialize()
    {
        base.Initialize();

        _currentAmmo = _data.MagazineCapacity;
        _layerMask = 1 << LayerMask.NameToLayer("Water"); // TODO 임시로 Water 레이어만 잡힘. 피격되는 물체 레이어로 교체할것
    }

    protected override void Shoot()
    {
        if (Time.time < lastFireTime + _data.DelayBetweenShots)
            return;

        if (_isReloading) return;

        if (_currentAmmo <= 0)
        {
            CartridgeEmpty(); 
            return;
        }

        lastFireTime = Time.time;
        _currentAmmo--;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject obj = ResourceManager.Instance.InstantiatePrefab("Bomb");
        obj.transform.position = gameObject.transform.position;
        obj.transform.rotation = Quaternion.identity;

        obj.GetComponent<Rigidbody>()?.AddForce(ray.direction * _data.ThrowPower);
    }


    private void Reload()
    {
        if (_isReloading || _currentAmmo >= _data.MagazineCapacity)
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

}
