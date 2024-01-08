using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UISceneTraining : UIScene
{
    #region Serialize Fields

    [SerializeField] private Sprite[] standings;

    #endregion

    #region Fields

    private GameObject options;
    private GameObject gunManage;

    private Image idleImage;
    private Image weaponImage;

    private TextMeshProUGUI magazineText;
    private TextMeshProUGUI maxMagazineText;

    private Button optionsBtn;
    private Button gunManageBtn;

    #endregion

    #region Initialize

    protected override void Init()
    {
        base.Init();

        options = ResourceManager.Instance.GetCache<GameObject>("UI_Popup_Options");
        gunManage = ResourceManager.Instance.GetCache<GameObject>("UI_Popup_GunManage");

        SetImages();
        SetTexts();
        SetButtons();
        SetEvents();

        WeaponEquipManager.Instance.InitWeapon();
    }

    private void SetImages()
    {
        SetUI<Image>();
        idleImage = GetUI<Image>("Image_Player_Idle");
        weaponImage = GetUI<Image>("Image_Player_Weapon");
    }

    private void SetTexts()
    {
        SetUI<TextMeshProUGUI>();
        magazineText = GetUI<TextMeshProUGUI>("Text_Magazine_Main");
        maxMagazineText = GetUI<TextMeshProUGUI>("Text_Magazine_Max");
    }

    private void SetButtons()
    {
        SetUI<Button>();
        optionsBtn = GetUI<Button>("Btn_Options");
        gunManageBtn = GetUI<Button>("Btn_GunManage");
    }

    private void SetEvents()
    {
        optionsBtn.gameObject.SetEvent(UIEventType.Click, OpenOptionsPopop);
        gunManageBtn.gameObject.SetEvent(UIEventType.Click, OpenGunManagePopup);
    }

    #endregion

    #region Button Event

    private void OpenOptionsPopop(PointerEventData eventData)
    {
        UI.ShowPopup<UIPopupOptions>(options);
    }

    private void OpenGunManagePopup(PointerEventData eventData)
    {
        UI.ShowPopup<UIPopupGunManage>(gunManage);
    }

    #endregion

    #region Update UI

    public void UpdateWeapon(WeaponData_Gun weaponData, float magazineModifier)
    {
        weaponImage.sprite = weaponData.ItemSprite;
        magazineText.text = (weaponData.MagazineCapacity+magazineModifier).ToString();
        maxMagazineText.text = (weaponData.MagazineCapacity+magazineModifier).ToString();
    }

    public void UpdateIdle(int index)
    {
        idleImage.sprite = standings[index];
    }

    public void UpdateMagazine(int magazine)
    {
        magazineText.text = magazine.ToString();
    }

    #endregion
}
