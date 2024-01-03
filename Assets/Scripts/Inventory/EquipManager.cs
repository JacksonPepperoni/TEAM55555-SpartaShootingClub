using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;
    private List<Weapon> _equipList;
    public event Action OnListChanged;
    private const int MaxEquipCount = 3;

    private void Awake()
    {
        Init();
    }

    public bool AddWeapon(WeaponData weaponData)
    {
        if(_equipList.Count >= MaxEquipCount)
        {
            return false;
        }
        else
        {
            Weapon weapon = new Weapon(weaponData);
            _equipList.Add(weapon);
            OnListChanged?.Invoke();
            return true;
        }
    }

    public WeaponData GetWeaponData(int index)
    {
        return _equipList[index].ItemData as WeaponData;
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

        _equipList = new List<Weapon>();
    }
}
