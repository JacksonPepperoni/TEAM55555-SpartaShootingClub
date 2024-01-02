using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Fields

    // �ӽ� ����
    [SerializeField] private GameObject testScene;

    private int order = 10;
    private Stack<UIPopup> popupStack = new();
    private UIScene sceneUI;

    #endregion

    #region Properties

    private GameObject uiroot;
    private GameObject UIRoot
    {
        get
        {
            if (uiroot == null)
            {
                uiroot = GameObject.Find("@UIRoot") ?? new GameObject("@UIRoot");
            }

            return uiroot;
        }
    }

    #endregion

    /// <summary>
    /// Scene, Popup ���� => ĵ���� �ʱ�ȭ
    /// </summary>
    /// <param name="uiObject">�ش� UI ������Ʈ</param>
    public void SetCanvas(GameObject uiObject)
    {
        // Canvas ������Ʈ ����
        var canvas = Util.GetOrAddComponent<Canvas>(uiObject);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        canvas.sortingOrder = order++;

        // Canvas Scaler ����
        var canvasScaler = Util.GetOrAddComponent<CanvasScaler>(uiObject);
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
    }

    private void Start()
    {
        // �ӽ� �� �׽�Ʈ ���� => ���ν� �Ŵ����� ���� ����
        ShowScene<UIScene>(testScene);
    }

    #region Scene

    public T ShowScene<T>(GameObject sceneObject) where T : UIScene
    {
        GameObject obj = Instantiate(sceneObject, UIRoot.transform);
        T sceneUI = Util.GetOrAddComponent<T>(obj);
        this.sceneUI = sceneUI; 
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
}
