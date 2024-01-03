using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupOptions : UIPopup
{
    private Button closedBtn;

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
        closedBtn.gameObject.SetEvent(UIEventType.Click, CloseOptionsPopup);
    }

    #endregion

    private void CloseOptionsPopup(PointerEventData eventData)
    {
        UI.ClosePopup(this);
    }
}
