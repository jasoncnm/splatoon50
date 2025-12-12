using TMPro;
using UnityEngine;

public class SFXValue : MonoBehaviour
{
    public GlobalGameStateSO gameGlobal;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        text.text = "SFX Volume: " + gameGlobal.SFXVolume + "%";
    }

    public void Value()
    {
        gameGlobal.SFXVolume += 25;

        if (gameGlobal.SFXVolume > 100) gameGlobal.SFXVolume = 0;

        text.text = "SFX Volume: " + gameGlobal.SFXVolume + "%";
    }

}
