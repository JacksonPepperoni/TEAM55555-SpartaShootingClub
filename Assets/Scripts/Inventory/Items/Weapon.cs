using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    private AccessoryContainer accessoryContainer;
    public WeaponItem(ItemData itemData) : base(itemData)
    {
        accessoryContainer = new AccessoryContainer();
    }
}
