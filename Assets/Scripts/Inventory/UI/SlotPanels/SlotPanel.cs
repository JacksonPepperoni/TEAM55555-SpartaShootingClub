using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotPanel : MonoBehaviour
{
    [SerializeField] private DescriptionPanel descriptionPanel;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private GameObject slotPrefab;

    protected List<Slot> _slotList;

    protected int MaxSlotLength;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        // 상속받은 곳에서 처리하는게 맞는데 일단 보류
        if(Inventory.Instance == null)
        {
            return;
        }

        LinkToData();
        RefreshPanel();
    }

    private void OnDisable()
    {
        UnLinkToData();
    }

    protected abstract void RefreshPanel();

    protected virtual void Init()
    {
        SetMaxSlotLength();

        _slotList = new List<Slot>();
        for(int i=0; i<MaxSlotLength; i++)
        {
            Slot slot = Instantiate(slotPrefab).GetComponent<Slot>();
            _slotList.Add(slot);
            _slotList[i].transform.SetParent(slotContainer);
            _slotList[i].GetData(i, descriptionPanel);
        }
    }
    protected abstract void LinkToData();
    protected abstract void UnLinkToData();

    protected abstract void SetMaxSlotLength();
    
}
