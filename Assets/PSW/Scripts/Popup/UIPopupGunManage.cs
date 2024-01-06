using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupGunManage : UIPopup
{
    #region Fields

    private Button closedBtn;
    private Button equipBtn;
    [SerializeField] private List<Toggle> GunToggles;
    [SerializeField] private List<Toggle> accessoryToggles;

    #endregion

    #region Initialize

    protected override void Init()
    {
        base.Init();
        SetButtons();
        SetEvents();
        SetToggles();
    }

    private void SetToggles()
    {
        SetUI<Toggle>();
        Toggle tempval;

        tempval = GetUI<Toggle>("Toggle_Weapon_AR");
        GunToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Weapon_SR");
        GunToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Weapon_SG");
        GunToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Weapon_MG");
        GunToggles.Add(tempval);

        tempval = GetUI<Toggle>("Toggle_Parts_Sight");
        accessoryToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Parts_Muzzle");
        accessoryToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Parts_Grip");
        accessoryToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Parts_Magazine");
        accessoryToggles.Add(tempval);
    }

    private void SetButtons()
    {
        SetUI<Button>();
        closedBtn = GetUI<Button>("Btn_Closed");
        equipBtn = GetUI<Button>("Button_Equip");
    }

    private void SetEvents()
    {
        closedBtn.gameObject.SetEvent(UIEventType.Click, CloseGunManagePopup);
        equipBtn.gameObject.SetEvent(UIEventType.Click, EquipWeapon);
    }

    #endregion

    private void CloseGunManagePopup(PointerEventData eventData)
    {
        UI.ClosePopup(this);
    }

    private void EquipWeapon(PointerEventData eventData)
    {
        int gunType = -1;
        WeaponData weaponData = null;
        for(int i=0; i < GunToggles.Count; i++)
        {
            if(GunToggles[i].isOn == true)
            {
                gunType = i;
                weaponData = GunToggles[i].GetComponent<UIGunItem>().GetWeaponData();
                break;
            }
        }

        for(int i=0; i < accessoryToggles.Count ; i++)
        {
            if(accessoryToggles[i].isOn == true)
            {
                switch(i)
                {
                    // Sight, Muzzle, Grip, Magazine
                    case 0: break; // change adsFOV
                    case 1: break; // change gunSound
                    case 2: break; // change grip
                    case 3: break; // change Magazine;
                }
            }
        }

        WeaponEquipManager.Instance.SetWeapon(gunType, weaponData as WeaponData_Gun);
        UI.ClosePopup(this);
    }
}
