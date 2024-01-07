using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipManager : Singleton<WeaponEquipManager>
{
    private Transform weaponCameraTF;
    private List<GameObject> weaponList;
    private Weapon_Gun currentWeapon;

    public Weapon_Gun CurrentWeapon => currentWeapon;
    public float DefaultAdsFOV { get; private set; }

    public override bool Initialize()
    {
        weaponCameraTF = Camera.main.transform.GetChild(0).transform;
        weaponList = new List<GameObject>
        {
            ResourceManager.Instance.GetCache<GameObject>("Gun_Handgun"),
            ResourceManager.Instance.GetCache<GameObject>("Gun_SniperRifle"),
            ResourceManager.Instance.GetCache<GameObject>("Gun_Shotgun"),
            ResourceManager.Instance.GetCache<GameObject>("Gun_MachineGun")
        };

        DefaultAdsFOV = CinemachineManager.Instance.ADSFOV;

        return base.Initialize();
    }

    public void InitWeapon()
    {
        SetWeapon(0);
    }

    public void SetWeapon(int index)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = Instantiate(weaponList[index], weaponCameraTF).GetComponent<Weapon_Gun>();
        
        // UI 세팅
        UISceneTraining scene = UIManager.Instance.SceneUI.GetComponent<UISceneTraining>();
        scene.UpdateWeapon(currentWeapon.Data);
    }
}
