using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIPanelAudio : UIElement
{
    #region Fields

    private Slider masterSlider;
    private Slider sfxSlider;
    private Slider uiSlider;

    private TextMeshProUGUI masterVolumeText;
    private TextMeshProUGUI sfxVolumeText;
    private TextMeshProUGUI uiVolumeText;

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
        sfxSlider = GetUI<Slider>("Slider_Audio_SFX");
        uiSlider = GetUI<Slider>("Slider_Audio_UI");
    }

    private void SetText()
    {
        SetUI<TextMeshProUGUI>();
        masterVolumeText = GetUI<TextMeshProUGUI>("Text_Volume_Master");
        sfxVolumeText = GetUI<TextMeshProUGUI>("Text_Volume_SFX");
        uiVolumeText = GetUI<TextMeshProUGUI>("Text_Volume_UI");
    }

    private void SetEvents()
    {
        Util.SetSliderUI(masterSlider, masterVolumeText, 100);
        Util.SetSliderUI(sfxSlider, sfxVolumeText, 100);
        Util.SetSliderUI(uiSlider, uiVolumeText, 100);

        masterSlider.gameObject.SetEvent(UIEventType.Click, ChangedMasterVolume);
        masterSlider.gameObject.SetEvent(UIEventType.Drag, ChangedMasterVolume);

        sfxSlider.gameObject.SetEvent(UIEventType.Click, ChangedSFXVolume);
        sfxSlider.gameObject.SetEvent(UIEventType.Drag, ChangedSFXVolume);

        uiSlider.gameObject.SetEvent(UIEventType.Click, ChangedUIVolume);
        uiSlider.gameObject.SetEvent(UIEventType.Drag, ChangedUIVolume);
    }

    #endregion

    #region Slider Events

    private void ChangedMasterVolume(PointerEventData eventData)
    {
        Util.SliderValueChanged(masterSlider, masterVolumeText);
    }

    private void ChangedSFXVolume(PointerEventData eventData)
    {
        Util.SliderValueChanged(sfxSlider, sfxVolumeText);
    }

    private void ChangedUIVolume(PointerEventData eventData)
    {
        Util.SliderValueChanged(uiSlider, uiVolumeText);
    }

    #endregion
}
