using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/Gun")]
public class GunDataSO : WeaponDataSO
{
    [Header("총 설정")]

    [SerializeField] private int _shotAtOnce; // 한번에 발사되는 총알 수.총알소모는 변수 관계없이 항상 1 
    [SerializeField] private float _spread; // 총이 얼만큼 퍼질지
    [SerializeField] private float _range; // 사정거리

    [SerializeField] GameObject _muzzleFlash;

    public int ShotAtOnce
    {
        private set { _shotAtOnce = value < 1 ? 1 : value; }
        get { return _shotAtOnce; }
    }

    public float Spread { get { return _spread; } }
    public float Range { get { return _range; } }
    public GameObject MuzzleFlash { get { return _muzzleFlash; } }
}
