using UnityEngine;
using UnityEngine.UI;

public class UISceneTraning : UIScene
{
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject gunManage;

    private Button optionsBtn;
    private Button gunManageBtn;

    #region Initialize

    protected override void Init()
    {
        base.Init();

        SetUI<Button>();
        optionsBtn = GetUI<Button>("Btn_Options");
        gunManageBtn = GetUI<Button>("Btn_GunManage");
        optionsBtn.onClick.AddListener(OpenOptionsPopop);
        gunManageBtn.onClick.AddListener(OpenGunManagePopup);
    }

    #endregion

    #region Button Event

    private void OpenOptionsPopop()
    {
        UI.ShowPopup<UIPopupOptions>(options);
    }

    private void OpenGunManagePopup()
    {
        UI.ShowPopup<UIPopupGunManage>(gunManage);
    }

    #endregion
}
