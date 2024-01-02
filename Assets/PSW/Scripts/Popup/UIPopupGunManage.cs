using UnityEngine.UI;

public class UIPopupGunManage : UIPopup
{
    private Button closedBtn;

    #region Initialize

    protected override void Init()
    {
        base.Init();
        SetUI<Button>();
        closedBtn = GetUI<Button>("Btn_Closed");
        closedBtn.onClick.AddListener(CloseGunManagePopup);
    }

    #endregion

    private void CloseGunManagePopup()
    {
        UI.ClosePopup(this);
    }
}
