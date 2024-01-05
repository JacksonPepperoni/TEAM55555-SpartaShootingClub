using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGunItem : UIBase
{
    #region Fields

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
        // 팝업 생성
        // 해당 오브젝트 위치를 기준으로 오른쪽에 생성
        Debug.Log("Enter");
    }

    private void ExitWeaponTooltip(PointerEventData eventData)
    {
        // 팝업 없애기
        Debug.Log("Exit");
    }

    #endregion
}
