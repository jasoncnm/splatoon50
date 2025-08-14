using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeUI : MonoBehaviour
{
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

        if (GameManager.money < cost) return;

        GameManager.money -= cost;

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
