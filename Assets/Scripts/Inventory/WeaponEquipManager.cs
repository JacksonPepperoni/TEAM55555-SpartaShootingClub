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
        SetWeapon(0, new AccModifier());
    }

    public void SetWeapon(int index, AccModifier accMod)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = Instantiate(weaponList[index], weaponCameraTF).GetComponent<Weapon_Gun>();
        currentWeapon.SetAccessory(accMod);
        
        // UI 세팅
        UISceneTraining scene = UIManager.Instance.SceneUI.GetComponent<UISceneTraining>();
        scene.UpdateWeapon(currentWeapon.Data, accMod.MagazineModifier);
    }
}

public class AccModifier
{
    public float AimModifier {get; private set;}
    public float SoundModifier {get; private set;}
    public float RecoilModifier {get; private set;}
    public int MagazineModifier {get; private set;}
    public AccModifier()
    {
        AimModifier = 1;
        SoundModifier = 1;
        RecoilModifier = 1;
        MagazineModifier = 0;
    }

    public void ChangeAim(float value)
    {
        AimModifier = value;
    }

    public void ChangeSound(float value)
    {
        SoundModifier = value;
    }

    public void ChangeRecoil(float value)
    {
        RecoilModifier = value;
    }

    public void ChangeMagazine(int value)
    {
        MagazineModifier = value;
    }
}