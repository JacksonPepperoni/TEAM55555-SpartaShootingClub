public class UIElement : UIBase
{
    protected override void Init()
    {
        base.Init();
    }

    public void DestroyElement()
    {
        Destroy(gameObject);
    }
}
