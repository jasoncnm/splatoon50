using UnityEngine;

public class PanelGroup : MonoBehaviour
{

    public GameObject[] panels;

    public UITabGroup tabGroup;

    public int panelIndex = 0;

    private void Awake()
    {
        ShowCurrentPanel();
    }

    void ShowCurrentPanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelIndex)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    public void SetPageIndex(int index)
    {
        panelIndex = index;
        ShowCurrentPanel();
    }
}
