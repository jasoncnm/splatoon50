using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeUI : MonoBehaviour
{
    public GlobalGameStateSO gameGlobal;
    public int maxPoint;
    public int cost;
    public TextMeshProUGUI text;
    
    int level = 1;
    int currentPoint = 0;

    public UnityEvent onLevelUp;

    private void Start()
    {
        UpdateText();
    }

    public void AddPoint()
    {

        if (gameGlobal.money < cost) return;

        gameGlobal.money -= cost;

        currentPoint++;

        if (currentPoint == maxPoint)
        {
            level++;
            currentPoint = 0;
            if (onLevelUp != null) onLevelUp.Invoke();
        }
        UpdateText();
    }

    void UpdateText()
    {
        text.text = currentPoint.ToString() + "\\" + maxPoint.ToString() + " (Level " + level.ToString() + ")";
    }

}
