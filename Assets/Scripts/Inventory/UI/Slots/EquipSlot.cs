using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : Slot
{
    protected override void DoubleClickAction()
    {
        _isDoubleClicked = true;
        Item item = EquipManager.Instance.GetWeapon(SlotIndex);
        Inventory.Instance.AddItem(item);
        EquipManager.Instance.RemoveWeapon(SlotIndex);
    }

    protected override void OpenDescriptionPanel()
    {
        if(_isDoubleClicked)
        {
            return;
        }
        Item item = EquipManager.Instance.GetWeapon(SlotIndex);
        DescriptionPanel.OpenPanel(item);

    }

}
