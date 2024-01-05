using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/Gun")]
public class GunDataSO : WeaponDataSO
{
    [Header("총 설정")]

    [SerializeField] private int _numberOfBulletFiredAtOnce; // 한번에 발사되는 총알수 최소 1이상 설정 ,총알소모는 변수 관계없이 항상 1 
    [SerializeField] private float _spread; // 총이 얼만큼 퍼질지
    [SerializeField] GameObject _muzzleFlash; // 총구 불

    [SerializeField] private float _rangeOfHits; // 사정거리, 폭발범위
    public int NumberOfBulletFiredAtOnce { get { return _numberOfBulletFiredAtOnce; } }
    public float Spread { get { return _spread; } }

    public GameObject MuzzleFlash { get { return _muzzleFlash; } }
    public float RangeOfHits { get { return _rangeOfHits; } }
}
