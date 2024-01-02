using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private string itemName;

    public ItemType ItemType => itemType;
    public string ItemName => itemName;
}

public enum ItemType
{
    None,
    Weapon,
    Accessory
}