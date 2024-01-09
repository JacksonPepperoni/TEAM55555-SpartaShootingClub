using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccessoryDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI accessoryName;
    [SerializeField] TextMeshProUGUI accessoryType;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image image;


    public void SetAccessoryInformation(AccessoryData accessoryData)
    {
        accessoryName.text = accessoryData.ItemName;
        accessoryType.text = accessoryData.AccessoryType.ToString();
        image.sprite = accessoryData.ItemSprite;
    }
}
