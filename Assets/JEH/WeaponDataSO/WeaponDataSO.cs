using UnityEngine;

public class WeaponDataSO : ScriptableObject
{
    [Header("무기 설정")]

    [SerializeField] private string _weaponName;
    [SerializeField] private Sprite _weaponIcon;

    [SerializeField] private float _damage;
    [SerializeField] private int _ammo; // 탄창수
    [SerializeField] private float _delayBetweenShots; //후딜
    [SerializeField] private float _reloadTime; // 리로딩시간
    [SerializeField] private bool _isContinuousShooting; // 누르고 있으면 연사가능한지
    [SerializeField] protected bool _isAutoReloading; // 리로딩 자동으로 하는지

    public string WeaponName { get { return _weaponName; } }
    public Sprite WeaponIcon { get { return _weaponIcon; } }
    public float Damage { get { return _damage; } }
    public int Ammo { get { return _ammo; } }
    public float DelayBetweenShots { get { return _delayBetweenShots; } }
    public float ReloadTime { get { return _reloadTime; } }
    public bool IsContinuousShooting { get { return _isContinuousShooting; } }
    public bool IsAutoReloading { get { return _isAutoReloading; } }


}
