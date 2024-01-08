using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UISceneTraining : UIScene
{
    #region Serialize Fields

    [SerializeField] private Sprite[] standings;

    [Header("Crosshair")]
    [SerializeField] private float moaValue;
    [SerializeField] private float margin;
    [SerializeField] private float speed;
    [SerializeField] private RectTransform[] crosshairLines;

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

    #region Unity

    private void Update()
    {
        float topValue, bottomValue, LeftValue, RightValue;

        topValue = Mathf.Lerp(crosshairLines[0].position.y, crosshair.position.y + margin + moaValue, speed * Time.deltaTime);
        bottomValue = Mathf.Lerp(crosshairLines[1].position.y, crosshair.position.y - margin - moaValue, speed * Time.deltaTime);
        LeftValue = Mathf.Lerp(crosshairLines[2].position.x, crosshair.position.x - margin - moaValue, speed * Time.deltaTime);
        RightValue = Mathf.Lerp(crosshairLines[3].position.x, crosshair.position.x + margin + moaValue, speed * Time.deltaTime);

        crosshairLines[0].position = new Vector2(crosshairLines[0].position.x, topValue);
        crosshairLines[1].position = new Vector2(crosshairLines[1].position.x, bottomValue);
        crosshairLines[2].position = new Vector2(LeftValue, crosshairLines[2].position.y);
        crosshairLines[3].position = new Vector2(RightValue, crosshairLines[3].position.y);

        if (moaValue > 50)
        {
            moaValue = 50;
        }

        if (moaValue > 0)
        {
            moaValue = Mathf.Lerp(moaValue, 0, 3f * Time.deltaTime);
        }
    }

    #endregion

    #region Update UI

    public void UpdateWeapon(WeaponData_Gun weaponData, float magazineModifier)
    {
        weaponImage.sprite = weaponData.ItemSprite;
        magazineText.text = (weaponData.MagazineCapacity + magazineModifier).ToString();
        maxMagazineText.text = (weaponData.MagazineCapacity + magazineModifier).ToString();
    }

    public void UpdateIdle(int index)
    {
        idleImage.sprite = standings[index];
    }

    public void UpdateMagazine(int magazine)
    {
        magazineText.text = magazine.ToString();
    }

    public void ShowCrosshair()
    {
        for (var i = 0; i < crosshair.childCount; i++)
        {
            crosshair.GetChild(i).gameObject.SetActive(true);
        }

        crosshair
            .DOScale(Vector3.one, 0.2f)
            .SetEase(Ease.InOutQuad);
    }

    public void HideCrosshair()
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

    public void UpdateCrosshair(float moa)
    {
        moaValue += moa * 100;
    }

    #endregion
}
