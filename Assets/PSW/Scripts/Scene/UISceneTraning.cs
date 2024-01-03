using UnityEngine;
using UnityEngine.EventSystems;
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
