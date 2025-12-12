using TMPro;
using UnityEngine;

public class MusicValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;


    public GlobalGameStateSO gameGlobal;

    private void Start()
    {
        text.text = "Music Volume: " + gameGlobal.musicVolume + "%";
    }

    public void Value()
    {
        gameGlobal.musicVolume += 25;

        if (gameGlobal.musicVolume > 100) gameGlobal.musicVolume = 0;

        text.text = "Music Volume: " + gameGlobal.musicVolume + "%";
    }



}
