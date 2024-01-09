using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedSlot : Slot
{
    // SlotIndex + 1 == (int) AccessoryType.

    private WeaponItem _currentWeapon;

    protected override void DoubleClickAction()
    {
        AccessoryType accessoryType = (AccessoryType) (SlotIndex+1);
        Item item = _currentWeapon.GetAttachedData(accessoryType);

        Inventory.Instance.AddItem(item);
        _currentWeapon.RemoveAccessory(accessoryType);
    }

    protected override void OpenDescriptionPanel()
    {
        // 더블클릭 할때만 효과 있음!!
    }

    public void GetCurrentWeapon(WeaponItem weaponItem)
    {
        _currentWeapon = weaponItem;
    }
}
