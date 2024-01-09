using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;
    private List<WeaponItem> _equipList;
    public event Action OnListChanged;
    private const int MaxEquipCount = 3;

    private void Awake()
    {
        Init();
    }

    public bool AddWeapon(WeaponItem weapon)
    {
        if(_equipList.Count >= MaxEquipCount)
        {
            return false;
        }
        else
        {
            _equipList.Add(weapon);
            OnListChanged?.Invoke();
            return true;
        }
    }

    public WeaponData GetWeaponData(int index)
    {
        return _equipList[index].ItemData as WeaponData;
    }

    public WeaponItem GetWeapon(int index)
    {
        return _equipList[index];
    }

    public void RemoveWeapon(int index)
    {
        _equipList.RemoveAt(index);
        OnListChanged?.Invoke();
    }

    public int GetWeaponCount()
    {
        return _equipList.Count;
    }

    private void Init()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _equipList = new List<WeaponItem>();
    }
    public void ChangeItem(int index)
    {
        if(_equipList.Count <= index)
        {
            return;
        }

        // TODO => 원래 데이터 삭제하고, 들어있는 WeaponItem 정보 꺼내서 주기
        // _equipList[index].SetWeaponStats() << 이거 호출해서 주는 식으로
    }
}
