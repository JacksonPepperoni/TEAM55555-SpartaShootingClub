using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGunTooltip : UIBase
{
    private TextMeshProUGUI _name;
    private TextMeshProUGUI _type;
    private TextMeshProUGUI _damage_accOpt;
    private TextMeshProUGUI _ammo_accVal;
    private TextMeshProUGUI _fireRate;
    private TextMeshProUGUI _range;

    private GameObject _weaponOption;

    private Image _image;

    protected override void Init()
    {
        base.Init();
        SetText();
        SetImage();
        SetGameObject();
    }

    private void SetText()
    {
        SetUI<TextMeshProUGUI>();
        _name = GetUI<TextMeshProUGUI>("Name");
        _type = GetUI<TextMeshProUGUI>("Type");
        _damage_accOpt = GetUI<TextMeshProUGUI>("Damage_AccOption");
        _ammo_accVal = GetUI<TextMeshProUGUI>("Ammo_AccValue");
        _fireRate = GetUI<TextMeshProUGUI>("FireRate");
        _range = GetUI<TextMeshProUGUI>("Range");
    }

    private void SetImage()
    {
        SetUI<Image>();
        _image = GetUI<Image>("Image");
    }

    private void SetGameObject()
    {
        SetUI<GameObject>();
        _weaponOption = GetUI<GameObject>("Fourth");
    }

    public void GetData(ItemData itemData)
    {
        switch (itemData.ItemType)
        {
            case ItemType.Weapon : SetTooltipContent(itemData as WeaponData); break;
            case ItemType.Accessory : SetTooltipContent(itemData as AccessoryData); break;
        }
    }

    private void SetTooltipContent(WeaponData weaponData)
    {
        _weaponOption.SetActive(true);

        _name.text = weaponData.ItemName;
        _type.text = "타입 : " + weaponData.ItemType.ToString();
        _damage_accOpt.text = "공격력 : " + weaponData.Damage.ToString();
        _ammo_accVal.text = "장탄 수 : " + weaponData.MagazineCapacity.ToString();

        _fireRate.text = "연사 속도 : " + weaponData.DelayBetweenShots.ToString() +"/s";
        _range.text = "사정거리 : " + weaponData.Range.ToString() + "m";

        _image.sprite = weaponData.ItemSprite;
    }

    private void SetTooltipContent(AccessoryData accessoryData)
    {
        _weaponOption.SetActive(false);

        _name.text = accessoryData.ItemName;
        _type.text = "타입 : " + accessoryData.ItemType.ToString();
        _damage_accOpt.text = TypeToOptStr(accessoryData.AccessoryType, out string oper);
        _ammo_accVal.text = accessoryData.Value.ToString() + oper;

        _image.sprite = accessoryData.ItemSprite;
    }

    private string TypeToOptStr(AccessoryType accessoryType, out string oper)
    {
        switch(accessoryType)
        {
            case AccessoryType.Sight: oper = "x"; return "조준 배율";
            case AccessoryType.Muzzle: oper = "x"; return "총기 소음";
            case AccessoryType.Grip: oper = "x"; return "총기 반동";
            case AccessoryType.Magazine: oper = "+"; return "장탄 수";
            default : oper = "" ; return "";
        }
    } 
}
