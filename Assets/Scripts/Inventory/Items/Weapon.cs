using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    private AccessoryContainer accessoryContainer;
    public Weapon(ItemData itemData) : base(itemData)
    {
        accessoryContainer = new AccessoryContainer();
    }
}
