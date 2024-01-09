public class UIPopup : UIBase
{
    protected override void Init()
    {
        base.Init();
        UI.SetCanvas(gameObject);
        InputManager.Instance.OnOpenUI();
    }

    protected virtual void OnDisable()
    {
        InputManager.Instance.OnCloseUI();
    }
}
