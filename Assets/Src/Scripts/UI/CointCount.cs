using TMPro;
using UnityEngine;

public class CointCount : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = GameManager.money.ToString();
    }

}
