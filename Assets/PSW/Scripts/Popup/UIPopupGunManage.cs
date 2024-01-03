using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupGunManage : UIPopup
{
    #region Fields

    private Button closedBtn;

    #endregion

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
        closedBtn = GetUI<Button>("Btn_Closed");
    }

    private void SetEvents()
    {
        closedBtn.gameObject.SetEvent(UIEventType.Click, CloseGunManagePopup);
    }

    #endregion

    private void CloseGunManagePopup(PointerEventData eventData)
    {
        UI.ClosePopup(this);
    }
}
