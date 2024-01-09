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

    protected bool _isDoubleClicked;

    private void Awake()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnFirstClick);
        Button.enabled = false;
        _isDoubleClicked = false;
    }

    private void OnFirstClick()
    {
        StartCoroutine(ActiveDoubleClick());
    }

    protected virtual IEnumerator ActiveDoubleClick()
    {
        _isDoubleClicked = false;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(DoubleClickAction);
        yield return new WaitForSeconds(0.15f);
        OpenDescriptionPanel();
        Button.onClick.RemoveListener(DoubleClickAction);
        Button.onClick.AddListener(OnFirstClick);
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
        itemImage.sprite = itemData.ItemSprite;
        Button.enabled = true;
    }

    public void ResetSlot()
    {
        Button.enabled = false;
        itemImage.sprite = null;
    }
}
