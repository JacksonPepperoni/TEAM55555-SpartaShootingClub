using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Item Datas")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private ItemType itemType;
    
    public string ItemName => itemName;
    public Sprite ItemSprite => itemSprite;
    public ItemType ItemType => itemType;
}

public enum ItemType
{
    None,
    Weapon,
    Accessory
}

public enum AccessoryType
{
    None,
    Sight,
    Muzzle,
    Grip,
    Magazine
}