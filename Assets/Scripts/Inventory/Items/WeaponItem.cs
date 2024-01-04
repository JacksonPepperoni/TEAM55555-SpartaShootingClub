using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public Dictionary<AccessoryType, AccessoryItem> CurrentParts {get; private set;}
    public event Action OnAttachChanged;

    public WeaponItem(ItemData itemData) : base(itemData)
    {
        CurrentParts = new Dictionary<AccessoryType, AccessoryItem>();
    }

    public bool TryAddAccessory(AccessoryItem accessoryItem)
    {

        AccessoryData accessoryData = accessoryItem.ItemData as AccessoryData;

        if(CurrentParts.ContainsKey(accessoryData.AccessoryType))
        {
            return false;
        }

        CurrentParts.Add(accessoryData.AccessoryType, accessoryItem);
        OnAttachChanged?.Invoke();
        return true;
    }

    public void RemoveAccessory(AccessoryType accessoryType)
    {
        CurrentParts.Remove(accessoryType);
        OnAttachChanged?.Invoke();
    }

    public Item GetAttachedData(AccessoryType accessoryType)
    {
        return CurrentParts[accessoryType];
    }

    public SumWeaponData GetWeaponStats()
    {
        SumWeaponData sumWeaponData = new SumWeaponData();

        sumWeaponData.damage = (ItemData as WeaponData).Damage;
        sumWeaponData.magazineCapacity = (ItemData as WeaponData).MagazineCapacity;

        foreach(AccessoryItem item in CurrentParts.Values)
        {
            AccessoryData accessoryData = item.ItemData as AccessoryData;
            CalculateAccessory(accessoryData.AccessoryType, accessoryData.Value, sumWeaponData);
        }

        return sumWeaponData;
    }

    public void CalculateAccessory(AccessoryType type, float value, SumWeaponData data)
    {
        switch(type)
        {
            case AccessoryType.Sight : data.aimMultiplier += value; break;
            case AccessoryType.Muzzle : data.fireSound += value; break;
            case AccessoryType.Grip : data.fireRecoil += value; break;
            case AccessoryType.Magazine : data.magazineCapacity += (int)value; break;
        }
    }

}

/// <summary>
/// 아이템에서 실제 무기로 데이터를 넘겨주기 위한 임시 클래스
/// </summary>
public class SumWeaponData
{
    public float damage;
    public float aimMultiplier;
    public float fireSound;
    public float fireRecoil;
    public int magazineCapacity;

    public SumWeaponData()
    {
        damage = 0;
        aimMultiplier = 1f;
        fireSound = 1f;
        fireRecoil = 1f;
        magazineCapacity = 0;
    }
}