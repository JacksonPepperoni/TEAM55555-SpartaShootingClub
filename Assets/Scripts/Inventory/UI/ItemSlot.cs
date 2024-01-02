using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    public Button Button { get; private set; }
    public int SlotIndex { get; private set; }

    private void Awake()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OpenDescriptionPanel);
    }

    private IEnumerator OnDoubleClick()
    {
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(EquipSlotItem);
        yield return new WaitForSeconds(0.2f);
        Button.onClick.RemoveListener(EquipSlotItem);
        Button.onClick.AddListener(OpenDescriptionPanel);
    }

    private void EquipSlotItem()
    {
        Button.enabled = false;
        // 인벤토리 접근 -> 장비로 바꾸기
    }

    private void OpenDescriptionPanel()
    {
        // TODO => 설명 패널 접근, 열기
        StartCoroutine(OnDoubleClick());
    }

    public void ChangeItemData(Item item)
    {
        Button.enabled = true;
        // TODO => 이미지 변경
    }

    public void GetIndex(int slotIndex)
    {
        SlotIndex = slotIndex;
    }

    public void ResetSlot()
    {
        Button.enabled = false;
        // TODO => 이미지 변경
    }
}
