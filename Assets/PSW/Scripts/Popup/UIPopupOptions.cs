using UnityEngine.UI;

public class UIPopupOptions : UIPopup
{
    private Button closedBtn;

    #region Initialize

    protected override void Init()
    {
        base.Init();
        SetUI<Button>();
        closedBtn = GetUI<Button>("Btn_Closed");
        closedBtn.onClick.AddListener(CloseOptionsPopup);
    }

    #endregion

    private void CloseOptionsPopup()
    {
        UI.ClosePopup(this);
    }
}
