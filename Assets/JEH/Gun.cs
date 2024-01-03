using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class Gun : Weapon
{


    private float _rangeOfHits = Mathf.Infinity; // 피격거리. 레이 길이, 데미지 입히는 거리

    private Coroutine _shotCoroutine;

    [SerializeField] private GameObject TestPrefab;
    [SerializeField] private GameObject TestPrefab22;

    private int _layerMask;


    private void OnEnable()
    {
        Initialize();
    }


    protected override void Initialize()
    {
        base.Initialize();

        _layerMask = 1 << LayerMask.NameToLayer("Water"); // Water 레이어만 잡힘
        _shotCoroutine = null;
    }

    private void Update()
    {

        if (weaponState != WeaponState.Reload)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentShotDelay <= 0)
                    ShotStart();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ShotStop();
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            Reloading();
        }


        _currentShotDelay -= Time.deltaTime;

    }


    #region 발사 관련

    private void ShotStart()
    {

        if (_shotCoroutine == null)
            _shotCoroutine = StartCoroutine(ShotCoroutine());

    }

    private IEnumerator ShotCoroutine()
    {
        while (true)
        {
            if (_currentCartridge <= 0)
            {
                if (_autoReloading)
                {
                    yield return StartCoroutine(ReloadingCoroutine());
                }
                else
                {
                    CartridgeEmpty();
                    yield break;
                }
            }

            Shot();

            if (!_isContinuousShooting)
            {
                ShotStop();
                yield break;
            }

            yield return new WaitForSeconds(_delayBetweenShots);
        }
    }

    private void ShotStop()
    {
        if (_shotCoroutine != null)
        {
            StopCoroutine(_shotCoroutine);
            _shotCoroutine = null;
        }

        weaponState = WeaponState.Idle;
    }

    private void Shot() // 레이로 발사
    {
        weaponState = WeaponState.Attack;

        _currentCartridge--;
        _currentShotDelay = _delayBetweenShots;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rangeOfHits, _layerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * _rangeOfHits, Color.red, 1);

            GameObject wallPiece = Instantiate(TestPrefab);
            wallPiece.transform.position = hit.point;
            wallPiece.transform.forward = hit.normal;

            GameObject dddd = Instantiate(TestPrefab22);
            dddd.transform.position = hit.point;
            dddd.transform.forward = hit.normal;


            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("바닥");
            }
            else if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("벽");
            }
        }
    }


    #endregion

    #region 탄약 리로드


    /// <summary>
    ///
    /// 움직이면서 리로드가능
    /// 리로딩 완료전에 무기 바꾸면 취소됨. 
    /// </summary>


    private void CartridgeEmpty()
    {
        Debug.Log("총알부족");
    }

    private void Reloading()
    {
        if (weaponState == WeaponState.Reload) return;

        ShotStop();
        StartCoroutine(ReloadingCoroutine());

        // TODO 이 총의 탄창이 남아있으면 재장전가능 없으면 FALSE 아니면 true
    }

    private IEnumerator ReloadingCoroutine()
    {
        if (weaponState == WeaponState.Reload) yield break;

        weaponState = WeaponState.Reload;

        Debug.Log("리로딩");
        yield return new WaitForSeconds(_ReloadDelay);

        // 소지탄창수--;
        _currentCartridge = _cartridge;
        _currentShotDelay = _delayBetweenShots;

        Debug.Log("리로드 완료");

        weaponState = WeaponState.Idle;
    }

    #endregion

}


