using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGunTooltip : UIBase
{
    #region Fields

    private ItemData itemData;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI typeText;
    private TextMeshProUGUI damageText;
    private TextMeshProUGUI magazineText;
    private TextMeshProUGUI fireRateText;
    private TextMeshProUGUI rangeText;

    private Image gunImage;

    #endregion

    protected override void Init()
    {
        base.Init();
        SetText();
        SetImage();

        gameObject.SetActive(false);
    }

    private void SetText()
    {
        SetUI<TextMeshProUGUI>();
        nameText = GetUI<TextMeshProUGUI>("Text_Name");
        typeText = GetUI<TextMeshProUGUI>("Text_Type");
        damageText = GetUI<TextMeshProUGUI>("Text_DamageAccOption");
        magazineText = GetUI<TextMeshProUGUI>("Text_AmmoAccValue");
        fireRateText = GetUI<TextMeshProUGUI>("Text_FireRate");
        rangeText = GetUI<TextMeshProUGUI>("Text_Range");
    }

    private void SetImage()
    {
        SetUI<Image>();
        gunImage = GetUI<Image>("Image_Gun");
    }

    public void SetData(ItemData itemData)
    {
        this.itemData = itemData;

        switch (this.itemData.ItemType)
        {
            case ItemType.Weapon: SetTooltipContent(this.itemData as WeaponData_Gun); break;
            case ItemType.Accessory: SetTooltipContent(this.itemData as AccessoryData); break;
        }
    }

    private void SetTooltipContent(WeaponData weaponData)
    {
        if (weaponData == null) return;

        nameText.text = weaponData.ItemName;
        typeText.text = $"타입 : {weaponData.ItemType}";
        damageText.text = $"공격력 : {weaponData.Damage}";
        magazineText.text = $"장탄 수 : {weaponData.MagazineCapacity}";
        fireRateText.text = $"연사 속도 : {weaponData.DelayBetweenShots}/s";
        rangeText.text = $"거리 : {weaponData.Range}M";

        fireRateText.enabled = true;
        rangeText.enabled = true;

        gunImage.sprite = weaponData.ItemSprite;
    }

    private void SetTooltipContent(AccessoryData accessoryData)
    {
        nameText.text = accessoryData.ItemName;
        typeText.text = $"타입 : {accessoryData.ItemType}";
        damageText.text = TypeToOptStr(accessoryData.AccessoryType, out string oper);
        magazineText.text = accessoryData.Value.ToString() + oper;

        fireRateText.enabled = false;
        rangeText.enabled = false;

        gunImage.sprite = accessoryData.ItemSprite;
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
