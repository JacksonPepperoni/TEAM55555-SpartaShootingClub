using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIPanelControl : UIElement
{
    #region Fields

    private Slider normalSenseSlider;
    private TextMeshProUGUI normalSenseText;

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
    }

    private void SetText()
    {
        SetUI<TextMeshProUGUI>();
        normalSenseText = GetUI<TextMeshProUGUI>("Text_Sensitivity_Normal");
    }

    private void SetEvents()
    {
        Util.SetSliderUI(normalSenseSlider, normalSenseText, SettingsManager.Instance.MouseSensitivity);

        normalSenseSlider.gameObject.SetEvent(UIEventType.Click, ChangedNormalSense);
        normalSenseSlider.gameObject.SetEvent(UIEventType.Drag, ChangedNormalSense);
        normalSenseSlider.gameObject.SetEvent(UIEventType.PointerUp, UpdateNormalSense);
    }

    #endregion

    #region Slider Events

    private void ChangedNormalSense(PointerEventData eventData)
    {
        Util.SliderValueChanged(normalSenseSlider, normalSenseText);
    }

    private void UpdateNormalSense(PointerEventData eventData)
    {
        SettingsManager.Instance.SetMouseSensitivity(normalSenseSlider.value);
    }

    #endregion
}
