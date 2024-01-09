using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPanel : SlotPanel
{
    protected override void RefreshPanel()
    {
        int count = EquipManager.Instance.GetWeaponCount();

        if(count > MaxSlotLength)
        {
            count = MaxSlotLength;
        }

        for(int i=0; i< count; i++)
        {
            ItemData itemData = EquipManager.Instance.GetWeaponData(i);
            _slotList[i].ChangeItemData(itemData);
        }

        for(int i=count; i < MaxSlotLength; i++)
        {
            _slotList[i].ResetSlot();
        }
    }

    protected override void SetMaxSlotLength()
    {
        MaxSlotLength = 3;
    }

    protected override void LinkToData()
    {
        EquipManager.Instance.OnListChanged -= RefreshPanel;
        EquipManager.Instance.OnListChanged += RefreshPanel;
    }

    protected override void UnLinkToData()
    {
        EquipManager.Instance.OnListChanged -= RefreshPanel;
    }
}
