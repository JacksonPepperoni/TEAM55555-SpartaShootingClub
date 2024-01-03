using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected string _WeaponName;
    protected Sprite _WeapoNIcon;
    protected Sprite _crosshairSprite;

    protected GameObject _projectilePrefab;

    protected float _damage;

    [Header("다음 발사까지 걸리는 시간, 후딜")]
    [SerializeField] protected float _delayBetweenShots; 
    protected float _currentShotDelay;

    [Header("총알 갯수")]
    [SerializeField] protected int _cartridge; 
    protected int _currentCartridge;

    [Header("리로딩 소요시간")]
    [SerializeField] protected float _ReloadDelay = 2;

    [Header("누르고 있으면 연사되는 무기인지")]
    [SerializeField] protected bool _isContinuousShooting = false;

    [Header("총알 다 쓰면 자동으로 재장전")]
    [SerializeField] protected bool _autoReloading = false;

    protected bool isReloading;
    protected bool isLeftPress;


    protected virtual void Initialize()
    {
        transform.localPosition = Vector3.zero;
        _currentShotDelay = _delayBetweenShots;
        _currentCartridge = _cartridge;

        isReloading = false;
        isLeftPress = false;
    }

}
