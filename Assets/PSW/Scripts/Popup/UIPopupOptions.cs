using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupOptions : UIPopup
{
    #region Fields

    private GameObject graphicPanel;
    private GameObject audioPanel;
    private GameObject controlPanel;

    private UIElement currentActivePanel;
    private Transform mainPanel;

    private Toggle graphicBtn;
    private Toggle audioBtn;
    private Toggle controlBtn;
    private Button closedBtn;

    #endregion

    #region Initialize

    protected override void Init()
    {
        base.Init();

        graphicPanel = ResourceManager.Instance.GetCache<GameObject>("UI_Panel_Graphic");
        audioPanel = ResourceManager.Instance.GetCache<GameObject>("UI_Panel_Audio");
        controlPanel = ResourceManager.Instance.GetCache<GameObject>("UI_Panel_Control");

        SetObject();
        SetButtons();
        SetEvents();
    }

    private void SetObject()
    {
        SetUI<Transform>();
        mainPanel = GetUI<Transform>("Main_Panel");
    }

    private void SetButtons()
    {
        SetUI<Button>();
        closedBtn = GetUI<Button>("Btn_Closed");

        SetUI<Toggle>();
        graphicBtn = GetUI<Toggle>("Toggle_Options_Graphic");
        audioBtn = GetUI<Toggle>("Toggle_Options_Audio");
        controlBtn = GetUI<Toggle>("Toggle_Options_Control");
    }

    private void SetEvents()
    {
        graphicBtn.gameObject.SetEvent(UIEventType.Click, ActiveGraphicPanel);
        audioBtn.gameObject.SetEvent(UIEventType.Click, ActiveAudioPanel);
        controlBtn.gameObject.SetEvent(UIEventType.Click, ActiveControlPanel);
        closedBtn.gameObject.SetEvent(UIEventType.Click, CloseOptionsPopup);

        currentActivePanel = UI.ShowElement<UIElement>(graphicPanel, mainPanel);
    }

    #endregion

    #region Button Events

    private void ActiveGraphicPanel(PointerEventData eventData)
    {
        if (currentActivePanel != null) currentActivePanel.DestroyElement();

        currentActivePanel = UI.ShowElement<UIElement>(graphicPanel, mainPanel);
    }

    private void ActiveAudioPanel(PointerEventData eventData)
    {
        if (currentActivePanel != null) currentActivePanel.DestroyElement();

        currentActivePanel = UI.ShowElement<UIElement>(audioPanel, mainPanel);
    }

    private void ActiveControlPanel(PointerEventData eventData)
    {
        if (currentActivePanel != null) currentActivePanel.DestroyElement();

        currentActivePanel = UI.ShowElement<UIElement>(controlPanel, mainPanel);
    }

    private void CloseOptionsPopup(PointerEventData eventData)
    {
        UI.ClosePopup(this);
    }

    #endregion
}
