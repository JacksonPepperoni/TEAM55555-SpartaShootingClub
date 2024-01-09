using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private List<Item> _inventoryList;
    public event Action OnListChanged;

    private void Awake()
    {
        Init();
    }

    public void AddItem(Item item)
    {
        _inventoryList.Add(item);
        OnListChanged?.Invoke();
    }

    public void RemoveItem(int index)
    {
        _inventoryList.RemoveAt(index);
        OnListChanged?.Invoke();
    }

    public ItemData GetItemData(int index)
    {
        return _inventoryList[index].ItemData;
    }

    public Item GetItem(int index)
    {
        return _inventoryList[index];
    }

    public int GetInventoryCount()
    {
        return _inventoryList.Count;
    }

    public bool EquipItem(int index)
    {
        if(_inventoryList[index] is WeaponItem
        && EquipManager.Instance.AddWeapon(_inventoryList[index] as WeaponItem))
        {
            RemoveItem(index);
            return true;
        }
        return false;
    }

    private void Init()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _inventoryList = new List<Item>();
    }




}
