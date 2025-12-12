using TMPro;
using UnityEngine;

public class CointCount : MonoBehaviour
{
    public GlobalGameStateSO gameGlobal;
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = gameGlobal.money.ToString();
    }

}
