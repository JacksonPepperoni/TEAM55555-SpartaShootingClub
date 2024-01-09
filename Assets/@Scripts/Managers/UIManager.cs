using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    #region Fields

    private int order = 10;
    private Stack<UIPopup> popupStack = new();

    #endregion

    #region Properties

    public GameObject UIRoot { get; private set; }
    public UIScene SceneUI { get; private set; }

    #endregion

    #region Init

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;
        UIRoot = GameObject.Find("@UIRoot") ?? new("@UIRoot");
        return true;
    }

    /// <summary>
    /// Scene, Popup 생성 => 캔버스 초기화
    /// </summary>
    /// <param name="uiObject">해당 UI 오브젝트</param>
    public void SetCanvas(GameObject uiObject)
    {
        // Canvas 컴포넌트 세팅
        var canvas = Util.GetOrAddComponent<Canvas>(uiObject);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        canvas.sortingOrder = order++;

        // Canvas Scaler 세팅
        var canvasScaler = Util.GetOrAddComponent<CanvasScaler>(uiObject);
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
    }

    #endregion

    #region Scene

    public T ShowScene<T>(GameObject sceneObject) where T : UIScene
    {
        GameObject scene = Instantiate(sceneObject, UIRoot.transform);
        T sceneUI = Util.GetOrAddComponent<T>(scene);
        SceneUI = sceneUI;
        return sceneUI;
    }

    #endregion

    #region Popup

    public T ShowPopup<T>(GameObject popupObject) where T : UIPopup
    {
        GameObject obj = Instantiate(popupObject, UIRoot.transform);
        T popup = Util.GetOrAddComponent<T>(obj);
        popupStack.Push(popup);
        
        return popup;
    }

    public void ClosePopup(UIPopup popup)
    {
        if (popupStack.Count == 0) return;

        if (popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopup();
    }

    public void ClosePopup()
    {
        if (popupStack.Count == 0) return;

        UIPopup popup = popupStack.Pop();
        Destroy(popup.gameObject);
        order--;
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
        {
            ClosePopup();
        }
    }

    #endregion

    #region Element

    public T ShowElement<T>(GameObject uiElement, Transform uiParent) where T : UIElement
    {
        GameObject element = Instantiate(uiElement, uiParent);
        T elementUI = Util.GetOrAddComponent<T>(element);
        return elementUI;
    }

    #endregion
}
