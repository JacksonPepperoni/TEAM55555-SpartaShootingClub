using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Weapon")]
public class WeaponData : ItemData
{
    [Header("Weapon Datas")]
    [SerializeField] private float damage;
    [SerializeField] private int magazineCapacity;
    // TODO => 총기 스탯 추가 필요!!

    public float Damage => damage;
    public int MagazineCapacity => magazineCapacity; 
}
