using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingButton : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private AccessoryData accessoryData;
    [SerializeField] private AccessoryData accessoryData2;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();        
    }

    private void Start()
    {
        _button.onClick.AddListener(GetItem);
        GetItem();
    }

    private void GetItem()
    {
        Item item1 = new WeaponItem(weaponData);
        Item item2 = new AccessoryItem(accessoryData);
        Item item3 = new AccessoryItem(accessoryData2);
        Inventory.Instance.AddItem(item1);
        Inventory.Instance.AddItem(item2);
        Inventory.Instance.AddItem(item3);

    }
}
