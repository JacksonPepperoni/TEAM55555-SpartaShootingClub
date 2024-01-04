using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISceneTraining : UIScene
{
    #region Fields

    private GameObject options;
    private GameObject gunManage;

    private Button optionsBtn;
    private Button gunManageBtn;

    #endregion

    #region Initialize

    protected override void Init()
    {
        base.Init();

        options = ResourceManager.Instance.GetCache<GameObject>("UI_Popup_Options");
        gunManage = ResourceManager.Instance.GetCache<GameObject>("UI_Popup_GunManage");

        SetButtons();
        SetEvents();
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
}
