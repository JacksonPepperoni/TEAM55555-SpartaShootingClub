using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    private float _rangeOfHits = Mathf.Infinity; // 피격거리. 레이 길이, 데미지 입히는 거리

    private Coroutine _shotCoroutine;
    private Coroutine _reloadCoroutine;

    [Space(15)]
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
        _reloadCoroutine = null;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isLeftPress = true;

            if (_currentShotDelay <= 0)
                ShotStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isLeftPress = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadStart();
        }

        _currentShotDelay -= Time.deltaTime;

    }


    #region 발사

    private void ShotStart()
    {
        if (isReloading)
            return;

        _shotCoroutine ??= StartCoroutine(ShotCoroutine());

    }

    private IEnumerator ShotCoroutine()
    {
        while (true)
        {
            if (_currentCartridge <= 0)
            {
                CartridgeEmpty();
                yield break;
            }

            Shot();

            if (_currentCartridge <= 0 && _autoReloading)
            {
                yield return StartCoroutine(ReloadCoroutine());
            }

            yield return new WaitForSeconds(_delayBetweenShots);

            if (!_isContinuousShooting)
            {
                ShotStop();
                yield break;
            }
            else
            {
                if (!isLeftPress)
                {
                    ShotStop();
                    yield break;
                }
            }
        }
    }

    private void ShotStop()
    {
        if (_shotCoroutine != null)
        {
            StopCoroutine(_shotCoroutine);
            _shotCoroutine = null;
        }
    }

    private void Shot() // 레이로 발사, 즉발
    {
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
               // Debug.Log("바닥");
            }
            else if (hit.collider.CompareTag("Wall"))
            {
              //  Debug.Log("벽");
            }
        }
    }


    #endregion

    #region 재장전

    private void CartridgeEmpty()
    {
        Debug.Log("총알이 부족해요");
    }

    private void ReloadStart() //BOOL로 만들어서 재장전 성공, 취소 판단할것
    {
        if (isReloading || _currentCartridge == _cartridge) // 총알꽉찬상태에선 리로드 불가
            return;

        isReloading = true;
        _reloadCoroutine ??= StartCoroutine(ReloadCoroutine());

    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        Debug.Log("리로딩");
        yield return new WaitForSeconds(_ReloadDelay);

        // 소지탄창수--;
        _currentCartridge = _cartridge;
        _currentShotDelay = _delayBetweenShots;

        Debug.Log("리로드 완료");

        isReloading = false;
        _reloadCoroutine = null;
    }

    private void ReloadStop() // 장전중에 무기바꾸면 취소되야함
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }

        isReloading = false;


    }


    #endregion

}


