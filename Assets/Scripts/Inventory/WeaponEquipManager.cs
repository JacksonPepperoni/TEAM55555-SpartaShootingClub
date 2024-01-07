using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipManager : MonoBehaviour
{
    public static WeaponEquipManager Instance;
    [SerializeField] private GameObject testWeaponPreFab;
    [SerializeField] private Transform weaponCameraTF;
    [SerializeField] private WeaponData_Gun firstWeaponData;

    private Weapon_Gun _currentWeapon;

    public Weapon_Gun CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        SetWeapon(0, firstWeaponData);
    }

    public void SetWeapon(int index, WeaponData_Gun weaponData_Gun)
    {
        // TODO => 두 매개 변수 중 하나는 필요없음!!
        if(_currentWeapon != null)
        {
            Destroy(_currentWeapon.gameObject);
        }

        _currentWeapon = Instantiate(testWeaponPreFab, weaponCameraTF).GetComponent<Weapon_Gun>();
        _currentWeapon.GetWeaponData(weaponData_Gun);
    }


}
