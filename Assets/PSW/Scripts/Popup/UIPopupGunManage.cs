using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupGunManage : UIPopup
{
    #region Fields

    private Button closedBtn;
    private Button equipBtn;
    private List<Toggle> GunToggles;
    private List<Toggle> accessoryToggles;

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

        GunToggles = new List<Toggle>();
        accessoryToggles = new List<Toggle>();

        tempval = GetUI<Toggle>("Toggle_Weapon_HG");
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
        for(int i=0; i < GunToggles.Count; i++)
        {
            if(GunToggles[i].isOn == true)
            {
                gunType = i;
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
                    case 0:
                    {
                        float defaultVal = WeaponEquipManager.Instance.DefaultAdsFOV;
                        CinemachineManager.Instance.ADSFOV = defaultVal * 0.5f;
                    }
                    break;
                    case 1: break; // change gunSound
                    case 2: break; // change grip
                    case 3: break; // change Magazine;
                }
            }
            else
            {
                switch(i)
                {
                    // Sight, Muzzle, Grip, Magazine
                    case 0:
                    {
                        float defaultVal = WeaponEquipManager.Instance.DefaultAdsFOV;
                        CinemachineManager.Instance.ADSFOV = defaultVal * 1f;
                    }
                    break;
                    case 1: break; // change gunSound
                    case 2: break; // change recoil TODO => SO var to Class var
                    case 3: break; // change Magazine TODO => SO var to Class var
                }
            }
        }

        WeaponEquipManager.Instance.SetWeapon(gunType);
        UI.ClosePopup(this);
    }
}
