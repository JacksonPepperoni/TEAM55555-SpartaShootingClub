using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() ?? obj.AddComponent<T>();
    }

    public static T InstantiateUI<T>(GameObject popupObject, Transform parent = null) where T : Component
    {
        var obj = GameObject.Instantiate(popupObject, parent);
        return GetOrAddComponent<T>(obj);
    }

    public static void DestroyUI(UIBase popup)
    {
        GameObject.Destroy(popup.gameObject);
    }

    public static void SetSliderUI(Slider slider, TextMeshProUGUI valueText, float value, float minValue = 0, float maxValue = 100)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;

        slider.value = value;
        valueText.text = value.ToString();
    }

    public static void SliderValueChanged(Slider slider, TextMeshProUGUI valueText)
    {
        float steppedValue = Mathf.Round(slider.value);
        slider.value = steppedValue;
        valueText.text = steppedValue.ToString();
    }
}
