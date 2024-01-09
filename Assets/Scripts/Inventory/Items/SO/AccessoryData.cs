using UnityEngine;

[CreateAssetMenu(fileName = "NewAccessory", menuName = "Item/Accessory")]
public class AccessoryData : ItemData
{
    [Header("Accessory Datas")]
    [SerializeField] private AccessoryType accessoryType;
    [SerializeField] private float value;
    
    public AccessoryType AccessoryType => accessoryType;
    public float Value => value;
}
