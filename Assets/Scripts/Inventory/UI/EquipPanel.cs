using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPanel : MonoBehaviour
{
    void Start()
    {
        Inventory.Instance.OnListChanged += RefreshPanel;
    }

    private void RefreshPanel()
    {

    }
}
