using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UISceneTraining : UIScene
{
    #region Serialize Fields

    [SerializeField] private Sprite[] standings;

    #endregion

    #region Fields

    private Transform crosshair;
    private Image idleImage;
    private Image weaponImage;
    private TextMeshProUGUI magazineText;
    private TextMeshProUGUI maxMagazineText;

    #endregion

    #region Initialize

    protected override void Init()
    {
        base.Init();

        SetObjects();
        SetImages();
        SetTexts();

        WeaponEquipManager.Instance.InitWeapon();
    }

    private void SetObjects()
    {
        SetUI<Transform>();
        crosshair = GetUI<Transform>("Crosshair");
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

    public void ShowCrossHair()
    {
        for (var i = 0; i < crosshair.childCount; i++)
        {
            crosshair.GetChild(i).gameObject.SetActive(true);
        }

        crosshair
            .DOScale(Vector3.one, 0.2f)
            .SetEase(Ease.InOutQuad);
    }

    public void HideCrossHair()
    {
        crosshair
            .DOScale(Vector3.zero, 0.2f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => 
            {
                for (var i = 0; i < crosshair.childCount; i++)
                {
                    crosshair.GetChild(i).gameObject.SetActive(false);
                }
            });
    }

    #endregion
}
