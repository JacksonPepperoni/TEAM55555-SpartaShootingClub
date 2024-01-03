using UnityEngine.UI;
using TMPro;

public class UIPanelAudio : UIElement
{
    private Slider masterSlider;
    private Slider sfxSlider;
    private Slider uiSlider;

    private TextMeshProUGUI masterVolumeText;
    private TextMeshProUGUI sfxVolumeText;
    private TextMeshProUGUI uiVolumeText;

    protected override void Init()
    {
        base.Init();
        SetSlider();
        SetText();
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

    #region Slider Events



    #endregion
}
