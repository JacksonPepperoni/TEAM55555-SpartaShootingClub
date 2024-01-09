using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] TextMeshProUGUI weaponDescriptionText;
    [SerializeField] Image weaponImage;
    [SerializeField] GameObject attachedSlotPrefab;
    [SerializeField] List<Transform> attachedTransforms;

    public WeaponItem CurrentWeapon {get; private set;}
    private List<AttachedSlot> _attachedSlots;

    private void Awake()
    {
        Init();

    }

    public void GetCurrentWeapon(WeaponItem weaponItem)
    {
        if(CurrentWeapon != null)
        {
            CurrentWeapon.OnAttachChanged -= RefreshSlots;
        }

        CurrentWeapon = weaponItem;

        SetWeaponInformation();

        CurrentWeapon.OnAttachChanged -= RefreshSlots;
        CurrentWeapon.OnAttachChanged += RefreshSlots;

        RefreshSlots();
    }

    private void SetWeaponInformation()
    {
        weaponImage.sprite = CurrentWeapon.ItemData.ItemSprite;
        weaponName.text = CurrentWeapon.ItemData.ItemName;
    }

    public void RefreshSlots()
    {
        var currentParts = CurrentWeapon.CurrentParts;

        for(int i=0; i<4; i++)
        {
            if(currentParts.ContainsKey((AccessoryType) i+1))
            {
                var accessoryData = currentParts[(AccessoryType) i+1].ItemData;
                _attachedSlots[i].ChangeItemData(accessoryData);
                _attachedSlots[i].GetCurrentWeapon(CurrentWeapon);
            }
            else
            {
                _attachedSlots[i].ResetSlot();
            }
        }
    }

    private void OnDisable()
    {
        if(CurrentWeapon != null)
        {
            CurrentWeapon.OnAttachChanged -= RefreshSlots;
        }
    }

    private void Init()
    {
        _attachedSlots = new List<AttachedSlot>();

        for(int i=0; i<4; i++)
        {
            AttachedSlot slot = Instantiate(attachedSlotPrefab).GetComponent<AttachedSlot>();
            slot.transform.SetParent(attachedTransforms[i]);
            slot.transform.localPosition = Vector2.zero;
            // Refactoring Required. => GetData (LSP)
            slot.GetData(i, null);
            _attachedSlots.Add(slot);
        }
    }

}
