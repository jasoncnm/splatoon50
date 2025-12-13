using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UITabGroup : MonoBehaviour
{

    public List<UITabButton> tabButtons { get; private set; }

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public Color tabPressed;

    public Color textIdle;
    public Color textHover;
    public Color textActive;

    public PanelGroup panelGroup;

    public bool isInit = false;

    UITabButton selectedTab;

    private void Start()
    {
        if (tabButtons != null && isInit) SetActive(0);
    }

    private void Update()
    {
#if false
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedTab.transform.GetSiblingIndex() > 0)
            {
                SetActive(selectedTab.transform.GetSiblingIndex() - 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedTab.transform.GetSiblingIndex() < (transform.childCount - 1))
            {
                SetActive(selectedTab.transform.GetSiblingIndex() + 1);
            }
        }
#endif
    }

    void SetActive(int index)
    {

        foreach(UITabButton button in tabButtons)
        {
            if (button.transform.GetSiblingIndex() == index)
            {
                OnTabSelected(button);
            }
        }
    }

    public void Subscribe(UITabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<UITabButton>();
        }

        tabButtons.Add(button);

    }

    public void OnTabEnter(UITabButton button)
    {
        ResetTabs();
        if (selectedTab == null || selectedTab != button)
        {
            if (button.background) button.background.color = tabHover;
            if (button.text) button.text.color = textHover;
        }
    }

    public void OnTabExit(UITabButton button)
    {
        ResetTabs();
    }


    public void OnTabDown(UITabButton button)
    {
        if (button.background) button.background.color = tabPressed;

    }

    public void OnTabUp(UITabButton button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }
        selectedTab = button;
        selectedTab.Select();

        ResetTabs();
        if (button.background) button.background.color = tabActive;

        if (button.text) button.text.color = textActive;

        if (panelGroup != null)
        {
            panelGroup.SetPageIndex(selectedTab.transform.GetSiblingIndex());
        }
    }

    public void OnTabSelected(UITabButton button)
    {


    }

    public void ResetTabs()
    {
        foreach(UITabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) continue;

            if (button.background) button.background.color = tabIdle;

            if (button.text) button.text.color = textIdle;

        }
    }

    public void DeselectAll()
    {
        selectedTab.Deselect();
        selectedTab = null;
        ResetTabs();
    }



}
