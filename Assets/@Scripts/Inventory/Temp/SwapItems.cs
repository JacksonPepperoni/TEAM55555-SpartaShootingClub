using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwapItems : MonoBehaviour
{
    // 임시 클래스입니다.

    public void SwapItem(int index)
    {
        // index == KeyInput(1,2,3) - 1
        // if press '2', Call SwapItem(1)
        if(EquipManager.Instance.GetWeaponCount() < index)
        {
            return;
        }
        
        SumWeaponData dat = EquipManager.Instance.GetWeapon(index).GetWeaponStats();
        // Create gun object.
        // Init datas using 'dat'
    }

    // 아래는 Ray 쏘는 부분

    private DropItem _curDropItem;
    private Camera _camera;
    private LayerMask _layerMask;

    private void Start()
    {
        _camera = Camera.main;
        _layerMask = LayerMask.GetMask("Water"); // TODO => "DropItem"
    }

    public void CheckItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 5f, _layerMask))
        {
            _curDropItem = hit.collider.GetComponent<DropItem>();
            // TODO => UI와 연결되어 있다면 획득 가능 현황 표시. 
        }
        else
        {
            _curDropItem = null;
        }
    }

    public void GetItem()
    {
        if (_curDropItem == null)
        {
            return;
        }

        // Item item = new Item(_curDropItem.ItemData);

        // Inventory.Instance.AddItem(_curDropItem.ItemData);
        Destroy(_curDropItem.gameObject);


    }
}
