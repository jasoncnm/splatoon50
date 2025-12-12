using System;
using TMPro;
using UnityEngine;

public class DebugTimer : MonoBehaviour
{

    public GlobalGameStateSO gameGlobal;
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeleft = gameGlobal.combatTime - gameGlobal.timer;

        TimeSpan time = TimeSpan.FromSeconds(timeleft);

        text.text = time.ToString("mm':'ss");

    }
}
