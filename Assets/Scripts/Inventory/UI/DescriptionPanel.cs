using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionPanel : MonoBehaviour
{
    [SerializeField] private AccessoryDescription accessoryDescription;
    [SerializeField] private WeaponDescription weaponDescription;
    [SerializeField] private GameObject ForeGround;

    public bool IsWeaponOpened {get; private set;}
    public WeaponDescription WeaponDescription => weaponDescription;

    public void OpenPanel(Item item)
    {
        ForeGround.SetActive(false);

        if(item is WeaponItem)
        {
            weaponDescription.gameObject.SetActive(true);
            accessoryDescription.gameObject.SetActive(false);
            weaponDescription.GetCurrentWeapon(item as WeaponItem);
            IsWeaponOpened = true;
        }
        else
        {
            weaponDescription.gameObject.SetActive(false);
            accessoryDescription.gameObject.SetActive(true);
            accessoryDescription.SetAccessoryInformation(item.ItemData as AccessoryData);
            IsWeaponOpened = false;
        }
    }

    private void OnEnable()
    {
        ForeGround.SetActive(true);
    }

    private void OnDisable()
    {
        IsWeaponOpened = false;
    }
}
