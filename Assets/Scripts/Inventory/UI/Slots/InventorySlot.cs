using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Slot
{
    protected override void DoubleClickAction()
    {
        if(Inventory.Instance.EquipItem(SlotIndex))
        {
            Button.enabled = false;
        }
    }

    protected override void OpenDescriptionPanel()
    {
        ItemData itemData = Inventory.Instance.GetItemData(SlotIndex);
        DescriptionPanel.OpenPanel(itemData);
        StartCoroutine(ActiveDoubleClick());
    }
}
