using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIPanelControl : UIElement
{
    #region Fields

    private Slider normalSenseSlider;
    private Slider aimSenseSlider;

    private TextMeshProUGUI normalSenseText;
    private TextMeshProUGUI aimSenseText;

    #endregion

    #region Init

    protected override void Init()
    {
        base.Init();
        SetSlider();
        SetText();
        SetEvents();
    }

    private void SetSlider()
    {
        SetUI<Slider>();
        normalSenseSlider = GetUI<Slider>("Slider_Sensitivity_Normal");
        aimSenseSlider = GetUI<Slider>("Slider_Sensitivity_Aim");
    }

    private void SetText()
    {
        SetUI<TextMeshProUGUI>();
        normalSenseText = GetUI<TextMeshProUGUI>("Text_Sensitivity_Normal");
        aimSenseText = GetUI<TextMeshProUGUI>("Text_Sensitivity_Aim");
    }

    private void SetEvents()
    {
        normalSenseSlider.gameObject.SetEvent(UIEventType.Click, ChangedNormalSense);
        normalSenseSlider.gameObject.SetEvent(UIEventType.Drag, ChangedNormalSense);

        aimSenseSlider.gameObject.SetEvent(UIEventType.Click, ChangedAimSense);
        aimSenseSlider.gameObject.SetEvent(UIEventType.Drag, ChangedAimSense);
    }

    #endregion

    #region Slider Events

    private void ChangedNormalSense(PointerEventData eventData)
    {
        Util.ValueStepChanged(100f, normalSenseSlider, normalSenseText);
    }

    private void ChangedAimSense(PointerEventData eventData)
    {
        Util.ValueStepChanged(100f, aimSenseSlider, aimSenseText);
    }

    #endregion
}
