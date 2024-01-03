using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected string _WeaponName;
    protected Sprite _WeapoNIcon;

    protected Sprite _crosshairSprite;

    [SerializeField] protected GameObject _projectilePrefab;

    protected float _damage;

    [SerializeField] protected float _delayBetweenShots; // 발사 간격
    protected float _currentShotDelay;

    [SerializeField] protected int _cartridge; // 탄창
    protected int _currentCartridge;

    protected float _ReloadDelay = 2;


    [SerializeField] protected bool _isContinuousShooting = false; // 누르고있으면 자동으로 발사되는지
    [SerializeField] protected bool _autoReloading = false; // 총알 다썼을때 자동으로 리로드 할건지


    protected virtual void Initialize()
    {
        transform.localPosition = Vector3.zero;
        _currentShotDelay = _delayBetweenShots;
        _currentCartridge = _cartridge;
    }

}
