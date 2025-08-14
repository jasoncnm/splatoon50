using TMPro;
using UnityEngine;

public class SFXValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        text.text = "SFX Volume: " + GameManager.SFXVolume + "%";
    }

    public void Value()
    {
        GameManager.SFXVolume += 25;

        if (GameManager.SFXVolume > 100) GameManager.SFXVolume = 0;

        text.text = "SFX Volume: " + GameManager.SFXVolume + "%";
    }

}
