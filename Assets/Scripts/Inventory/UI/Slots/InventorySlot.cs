using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Slot
{
    protected override void DoubleClickAction()
    {
        _isDoubleClicked = true;
        Item item = Inventory.Instance.GetItem(SlotIndex);

        if (item is WeaponItem)
        {
            Inventory.Instance.EquipItem(SlotIndex);
        }
        else // 부착물의 경우
        {
            if (!DescriptionPanel.IsWeaponOpened)
            {
                return;
            }
            else if(DescriptionPanel.WeaponDescription.CurrentWeapon.TryAddAccessory(item as AccessoryItem))
            {
                Inventory.Instance.RemoveItem(SlotIndex);
            }
        }
    }

    protected override void OpenDescriptionPanel()
    {
        if (_isDoubleClicked)
        {
            return;
        }

        Item item = Inventory.Instance.GetItem(SlotIndex);
        DescriptionPanel.OpenPanel(item);
    }
}
