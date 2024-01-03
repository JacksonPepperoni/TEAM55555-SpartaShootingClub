using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupOptions : UIPopup
{
    #region Fields

    [SerializeField] private GameObject graphicPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject controlPanel;

    private UIElement currentActivePanel;
    private Transform mainPanel;

    private Button graphicBtn;
    private Button audioBtn;
    private Button controlBtn;
    private Button closedBtn;

    #endregion

    #region Initialize

    protected override void Init()
    {
        base.Init();
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
        graphicBtn = GetUI<Button>("Btn_Options_Graphic");
        audioBtn = GetUI<Button>("Btn_Options_Audio");
        controlBtn = GetUI<Button>("Btn_Options_Control");
        closedBtn = GetUI<Button>("Btn_Closed");
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
