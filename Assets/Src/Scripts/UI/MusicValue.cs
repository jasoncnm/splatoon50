using TMPro;
using UnityEngine;

public class MusicValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;


    private void Start()
    {
        text.text = "Music Volume: " + GameManager.musicVolume + "%";
    }

    public void Value()
    {
        GameManager.musicVolume += 25;

        if (GameManager.musicVolume > 100) GameManager.musicVolume = 0;

        text.text = "Music Volume: " + GameManager.musicVolume + "%";
    }



}
