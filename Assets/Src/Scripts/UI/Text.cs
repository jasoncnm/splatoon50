using TMPro;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI targetText;

    public void SetText(string text)
    {
        targetText.text = text;
    }

}
