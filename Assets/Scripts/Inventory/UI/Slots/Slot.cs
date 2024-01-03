using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    public DescriptionPanel DescriptionPanel { get; private set;}
    public Button Button { get; private set; }
    public int SlotIndex { get; private set; }

    private void Awake()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OpenDescriptionPanel);
        Button.enabled = false;
    }

    protected virtual IEnumerator ActiveDoubleClick()
    {
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(DoubleClickAction);
        yield return new WaitForSeconds(0.2f);
        Button.onClick.RemoveListener(DoubleClickAction);
        Button.onClick.AddListener(OpenDescriptionPanel);
    }

    protected abstract void DoubleClickAction();

    protected abstract void OpenDescriptionPanel();

    public void GetData(int slotIndex, DescriptionPanel descriptionPanel)
    {
        SlotIndex = slotIndex;
        DescriptionPanel = descriptionPanel;
    }

    public void ChangeItemData(ItemData itemData)
    {
        Button.enabled = true;
        itemImage.sprite = itemData.ItemSprite;
    }

    public void ResetSlot()
    {
        Button.enabled = false;
        itemImage.sprite = null;
    }
}
