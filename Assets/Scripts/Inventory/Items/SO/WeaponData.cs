using UnityEngine;

public class WeaponData : ItemData
{
    [Header("Weapon Datas")]
    [SerializeField] private float _damage;
    [SerializeField] private int _magazineCapacity;
    // TODO => 총기 스탯 추가 필요!!

    [SerializeField] private float _castingDelay; // 선딜
    [SerializeField] private float _delayBetweenShots; //후딜
    [SerializeField] private float _reloadTime; // 장전시간 (장전 애니모션 시간)

    [SerializeField] private bool _isContinuousShooting; // 클릭하고 있으면 연사되는지
    [SerializeField] protected bool _isAutoReloading; // 재장전이 자동으로 되는지

    public float Damage => _damage;
    public int MagazineCapacity => _magazineCapacity;
    public float CastingDelay => _castingDelay;
    public float DelayBetweenShots => _delayBetweenShots;
    public float ReloadTime => _reloadTime;
    public bool IsContinuousShooting => _isContinuousShooting;
    public bool IsAutoReloading => _isAutoReloading;
}
