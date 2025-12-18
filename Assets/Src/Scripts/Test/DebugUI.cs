using System;
using UnityEngine;

public class DebugUI : MonoBehaviour
{

    static DebugUI instance;

    public GUIStyle stateTextStyle;
    public GUIStyle textStyle;
        
    public GlobalGameStateSO gameGlobal;

    bool showGui = true;
    bool isDown = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) && !isDown)
        {
            isDown = true;
            showGui = !showGui;
        }

        if (Input.GetKeyUp(KeyCode.BackQuote))
        {
            isDown = false;
        }

        if (!showGui) return;

        Rect textRegion = new Rect(0, 0, Screen.width, Screen.height);

        float timeleft = gameGlobal.combatTime - GameManager.instance.timer;
        TimeSpan time = TimeSpan.FromSeconds(timeleft);

        PlayerGunController playerGunController = FindAnyObjectByType<PlayerGunController>();
        if (playerGunController != null)
        {
            int bulletCount = playerGunController.bulletLeft;
            int maxBullet = playerGunController.maxBullet;

            string bulletText = "Bullet: " + bulletCount.ToString() + " / " + maxBullet.ToString() + "\n";
            string timeLeftText = time.ToString("mm':'ss") + "\n";
            string waveText = "Wave: " + GameManager.instance.wave.ToString() + "\n";
            string outputText = bulletText + waveText + timeLeftText;

            GUI.Label(textRegion, outputText, textStyle);
        }

        string stateText = gameGlobal.gameState.ToString();

        GUI.Label(textRegion, stateText, stateTextStyle);

    }

}
