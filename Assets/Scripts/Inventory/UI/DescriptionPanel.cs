using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionPanel : MonoBehaviour
{
    [SerializeField] private AccessoryDescription accessoryDescription;
    [SerializeField] private WeaponDescription weaponDescription;
    [SerializeField] private GameObject ForeGround;

    public void OpenPanel(ItemData itemData)
    {
        // TODO.
        // 3. 정보를 토대로 정보 출력 메서드 호출

        ForeGround.SetActive(false);

        if(itemData is AccessoryData)
        {
            weaponDescription.gameObject.SetActive(false);
            accessoryDescription.gameObject.SetActive(true);
        }
        else
        {
            weaponDescription.gameObject.SetActive(true);
            accessoryDescription.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ForeGround.SetActive(true);
    }
}
