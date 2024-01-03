using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : Slot
{
    protected override void DoubleClickAction()
    {
        ItemData itemData = EquipManager.Instance.GetWeaponData(SlotIndex);
        Inventory.Instance.AddItem(itemData);
        EquipManager.Instance.RemoveWeapon(SlotIndex);
    }

    protected override void OpenDescriptionPanel()
    {
        ItemData itemData = EquipManager.Instance.GetWeaponData(SlotIndex);
        DescriptionPanel.OpenPanel(itemData);
        StartCoroutine(ActiveDoubleClick());
    }
}
