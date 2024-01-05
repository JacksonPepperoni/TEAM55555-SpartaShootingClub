using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

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
        Util.SetSliderUI(normalSenseSlider, normalSenseText, SettingsManager.Instance.MouseSensitivity);
        Util.SetSliderUI(aimSenseSlider, aimSenseText, 50);

        normalSenseSlider.gameObject.SetEvent(UIEventType.Click, ChangedNormalSense);
        normalSenseSlider.gameObject.SetEvent(UIEventType.Drag, ChangedNormalSense);
        normalSenseSlider.gameObject.SetEvent(UIEventType.PointerUp, UpdateNormalSense);

        aimSenseSlider.gameObject.SetEvent(UIEventType.Click, ChangedAimSense);
        aimSenseSlider.gameObject.SetEvent(UIEventType.Drag, ChangedAimSense);
    }

    #endregion

    #region Slider Events

    private void ChangedNormalSense(PointerEventData eventData)
    {
        Util.SliderValueChanged(normalSenseSlider, normalSenseText);
    }

    private void ChangedAimSense(PointerEventData eventData)
    {
        Util.SliderValueChanged(aimSenseSlider, aimSenseText);
    }

    private void UpdateNormalSense(PointerEventData eventData)
    {
        SettingsManager.Instance.MouseSensitivity = normalSenseSlider.value;
    }

    #endregion
}
