using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    [Header("피격거리")]
    [SerializeField] private float _rangeOfHits = 100;

    [Header("한번에 발사되는 총알수")] // 최소 1이상 설정 ,총알소모는 변수 관계없이 항상 1 
    [SerializeField] private int _shotsAtOnce;

    [Header("총알이 얼마나 퍼질지")]
    [SerializeField] private float _spread;

    private Coroutine _shotCoroutine;
    private Coroutine _reloadCoroutine;

    private int _layerMask;

    [SerializeField] private GameObject TestPrefab;
    [SerializeField] private GameObject TestPrefab22;


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

        for (int i = 0; i < _shotsAtOnce; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            float x = Random.Range(-_spread, _spread);
            float y = Random.Range(-_spread, _spread);
            Vector3 dir = new Vector3(x, y, 0);

            if (Physics.Raycast(ray.origin, (ray.direction + dir).normalized, out hit, _rangeOfHits, _layerMask))
            {
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

            Debug.DrawRay(ray.origin, (ray.direction + dir).normalized * _rangeOfHits, Color.red, 1);

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


