using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingButton : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private AccessoryData accessoryData;
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
        Inventory.Instance.AddItem(weaponData);
        Inventory.Instance.AddItem(accessoryData);
    }
}
