using UnityEngine;

public class WeaponEquipManager : Singleton<WeaponEquipManager>
{
    [SerializeField] private GameObject testWeaponPreFab;
    [SerializeField] private Transform weaponCameraTF;
    [SerializeField] private WeaponData_Gun firstWeaponData;

    private Weapon_Gun _currentWeapon;

    public Weapon_Gun CurrentWeapon => _currentWeapon;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        return true;
    }

    public void InitWeapon()
    {
        SetWeapon(firstWeaponData);
    }

    public void SetWeapon(WeaponData_Gun weaponData_Gun)
    {
        if(_currentWeapon != null)
        {
            Destroy(_currentWeapon.gameObject);
        }

        _currentWeapon = Instantiate(testWeaponPreFab, weaponCameraTF).GetComponent<Weapon_Gun>();
        _currentWeapon.GetWeaponData(weaponData_Gun);

        // UI 세팅
        UISceneTraining scene = UIManager.Instance.SceneUI.GetComponent<UISceneTraining>();
        scene.UpdateWeapon(weaponData_Gun);
    }
}
