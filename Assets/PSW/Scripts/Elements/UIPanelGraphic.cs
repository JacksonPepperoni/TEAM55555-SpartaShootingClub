using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIPanelGraphic : UIElement
{
    #region Fields

    private Slider fovSlider;
    private TextMeshProUGUI fovText;

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
        fovSlider = GetUI<Slider>("Slider_Graphic_Fov");
    }

    private void SetText()
    {
        SetUI<TextMeshProUGUI>();
        fovText = GetUI<TextMeshProUGUI>("Text_Graphic_Fov");
    }

    private void SetEvents()
    {
        Util.SetSliderUI(fovSlider, fovText, SettingsManager.Instance.FOV, 80, 110);

        fovSlider.gameObject.SetEvent(UIEventType.Click, ChangedFOV);
        fovSlider.gameObject.SetEvent(UIEventType.Drag, ChangedFOV);
        fovSlider.gameObject.SetEvent(UIEventType.PointerUp, UpdateFOV);
    }

    #endregion

    #region Slider Events

    private void ChangedFOV(PointerEventData eventData)
    {
        Util.SliderValueChanged(fovSlider, fovText);
    }

    private void UpdateFOV(PointerEventData eventData)
    {
        SettingsManager.Instance.FOV = fovSlider.value;
    }

    #endregion
}
