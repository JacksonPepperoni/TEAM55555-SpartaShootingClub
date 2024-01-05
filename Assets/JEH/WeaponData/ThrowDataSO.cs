using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/Throw")]
public class ThrowDataSO : WeaponDataSO
{
    [Header("투척 설정")]
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _power; //던지는힘

    public GameObject Projectile { get { return _projectile; } }
    public float Power { get { return _power; } }



}
