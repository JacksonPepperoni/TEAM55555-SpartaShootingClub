using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIPanelAudio : UIElement
{
    #region Fields

    private Slider masterSlider;

    private TextMeshProUGUI masterVolumeText;

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
        masterSlider = GetUI<Slider>("Slider_Audio_Master");
    }

    private void SetText()
    {
        SetUI<TextMeshProUGUI>();
        masterVolumeText = GetUI<TextMeshProUGUI>("Text_Volume_Master");
    }

    private void SetEvents()
    {
        Util.SetSliderUI(masterSlider, masterVolumeText, SettingsManager.Instance.MasterVolume);

        masterSlider.gameObject.SetEvent(UIEventType.Click, ChangedMasterVolume);
        masterSlider.gameObject.SetEvent(UIEventType.Drag, ChangedMasterVolume);
        masterSlider.gameObject.SetEvent(UIEventType.PointerUp, UpdateMasterVolume);
    }

    #endregion

    #region Slider Events

    private void ChangedMasterVolume(PointerEventData eventData)
    {
        Util.SliderValueChanged(masterSlider, masterVolumeText);
    }

    private void UpdateMasterVolume(PointerEventData eventData)
    {
        SettingsManager.Instance.SetMasterVolume(masterSlider.value);
    }

    #endregion
}
