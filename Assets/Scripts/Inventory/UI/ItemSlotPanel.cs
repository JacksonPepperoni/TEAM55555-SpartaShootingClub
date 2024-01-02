using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotPanel : MonoBehaviour
{
    [SerializeField] private DescriptionPanel descriptionPanel;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private GameObject itemSlotPrefab;

    private List<ItemSlot> _itemSlotList;

    // TODO => 변수 처리 제대로!!
    private int MaxSlotLength;

    private void Awake()
    {
        MaxSlotLength = 20;
        
        _itemSlotList = new List<ItemSlot>();
        for(int i=0; i<20; i++)
        {
            ItemSlot itemSlot = Instantiate(itemSlotPrefab).GetComponent<ItemSlot>();
            _itemSlotList.Add(itemSlot);
            itemSlot.transform.SetParent(itemSlotContainer);
            itemSlot.GetIndex(i);
        }
    }

    private void Start()
    {
        OnEnable();
        Inventory.Instance.OnListChanged += RefreshPanel;
    }

    private void OnEnable()
    {

    }

    private void RefreshPanel()
    {
        int count = Inventory.Instance.GetInventoryCount();

        for(int i=0; i< MaxSlotLength; i++)
        {
            Item item = Inventory.Instance.GetInventoryItem(i);
            _itemSlotList[i].ChangeItemData(item);
        }

        for(int i=count; i < MaxSlotLength; i++)
        {
            _itemSlotList[i].ResetSlot();
        }
    }
}
