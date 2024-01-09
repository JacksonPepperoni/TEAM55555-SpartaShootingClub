public class UIScene : UIBase
{
    protected override void Init()
    {
        base.Init();
        UI.SetCanvas(gameObject);
    }
}
