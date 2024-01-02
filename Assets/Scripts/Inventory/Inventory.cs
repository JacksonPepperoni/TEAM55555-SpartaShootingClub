using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private List<Item> _inventoryList;
    private List<Item> _equipList;
    public event Action OnListChanged;

    private void Awake()
    {
        Init();
    }

    public void RemoveInventoryItem(int index)
    {
        if(index < 0)
        {
            Debug.Log("인벤토리 인덱스 오류");
            return;
        }

        _inventoryList.RemoveAt(index);
        OnListChanged?.Invoke();
    }

    public Item GetInventoryItem(int index)
    {
        if(index < 0)
        {
            Debug.Log("인벤토리 인덱스 오류");
            return null;
        }

        return null;
    }

    // TODO => EquipList 처리 필요
    // public void EquipItem(int index)
    // {
    //     _equipList.Add(_inventoryList[index]);
    //     RemoveItem(index);
    //     OnListChanged?.Invoke();
    // }

    public int GetInventoryCount()
    {
        return _inventoryList.Count;
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
