using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Weapon/Gun")]
public class WeaponData_Gun : WeaponData
{
    [Header("총 설정")]

    [SerializeField] private int _shotAtOnce; // 한번에 발사되는 총알 수.총알소모는 변수 관계없이 항상 1 
    [SerializeField] private float _spread; // 얼만큼 퍼져서 발사되는지
    [SerializeField] private float _range; // 사정거리

    [SerializeField] GameObject _muzzleFlash;

    public int ShotAtOnce // 최소값 1
    {
        private set { _shotAtOnce = value < 1 ? 1 : value; }
        get { return _shotAtOnce; }
    }

    public float Spread => _spread;
    public float Range => _range;
    public GameObject MuzzleFlash => _muzzleFlash;
}
