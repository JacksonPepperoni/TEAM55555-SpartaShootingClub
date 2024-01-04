using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum UIEventType
{
    Click, PointerEnter, PointerExit, PointerDown, Drag,
}

public static class UIEvent
{
    public static void SetEvent(this GameObject gameObject, UIEventType uiEventType, Action<PointerEventData> action)
    {
        UIEventHandler handler = Util.GetOrAddComponent<UIEventHandler>(gameObject);
        handler.BindEvent(uiEventType, action);
    }
}
