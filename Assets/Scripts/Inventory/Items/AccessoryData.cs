using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAccessory", menuName = "Item/Accessory")]
public class AccessoryData : ItemData
{
    [Header("Accessory Datas")]
    [SerializeField] private AccessoryType accessoryType;
    
    public AccessoryType AccessoryType => accessoryType;
}
