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

    public static void ValueStepChanged(float stepSize, Slider slider, TextMeshProUGUI valueText)
    {
        float steppedValue = Mathf.Round(slider.value * stepSize);
        slider.value = steppedValue * 0.01f;
        valueText.text = steppedValue.ToString();
    }
}
