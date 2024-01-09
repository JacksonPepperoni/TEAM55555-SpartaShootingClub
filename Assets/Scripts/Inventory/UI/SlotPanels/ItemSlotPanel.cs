using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSlotPanel : SlotPanel
{
    protected override void RefreshPanel()
    {
        int count = Inventory.Instance.GetInventoryCount();

        if(count > MaxSlotLength)
        {
            count = MaxSlotLength;
        }

        for(int i=0; i< count; i++)
        {
            ItemData itemData = Inventory.Instance.GetItemData(i);
            _slotList[i].ChangeItemData(itemData);
        }

        for(int i=count; i < MaxSlotLength; i++)
        {
            _slotList[i].ResetSlot();
        }
    }

    protected override void LinkToData()
    {
        Inventory.Instance.OnListChanged -= RefreshPanel;
        Inventory.Instance.OnListChanged += RefreshPanel;
    }

    protected override void UnLinkToData()
    {
        Inventory.Instance.OnListChanged -= RefreshPanel;
    }

    protected override void SetMaxSlotLength()
    {
        MaxSlotLength = 20;
    }

}
