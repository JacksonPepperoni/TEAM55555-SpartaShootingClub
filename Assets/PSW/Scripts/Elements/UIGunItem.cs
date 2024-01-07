using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGunItem : UIBase
{
    #region Fields

    [SerializeField] private Transform tooltipPanel;
    [SerializeField] private ItemData gunData;
    [SerializeField] private GameObject tooltip;

    private Toggle toggle;

    #endregion

    #region Init

    protected override void Init()
    {
        base.Init();

        toggle = GetComponent<Toggle>();
        SetEvents();
    }

    private void SetEvents()
    {
        toggle.gameObject.SetEvent(UIEventType.PointerEnter, EnterWeaponTooltip);
        toggle.gameObject.SetEvent(UIEventType.PointerExit, ExitWeaponTooltip);
    }

    #endregion

    #region Tootlip Events

    private void EnterWeaponTooltip(PointerEventData eventData)
    {
        if (tooltip == null) return;
        tooltip.SetActive(true);
        tooltip.transform.localPosition = transform.localPosition + new Vector3(-300, 0, 0);
        tooltip.GetComponent<UIGunTooltip>().SetData(gunData);
    }

    private void ExitWeaponTooltip(PointerEventData eventData)
    {
        if (tooltip == null) return;
        tooltip.gameObject.SetActive(false);
    }

    #endregion
}
