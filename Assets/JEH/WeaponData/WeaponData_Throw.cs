using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Weapon/Throw")]
public class WeaponData_Throw : WeaponData
{
    [Header("투척 설정")]

    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _throwPower;

    public GameObject Projectile => _projectile;
    public float ThrowPower => _throwPower;



}
