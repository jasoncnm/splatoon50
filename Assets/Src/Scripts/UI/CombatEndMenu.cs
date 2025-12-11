using UnityEngine;

public class CombatEndMenu : MonoBehaviour
{

    Transform frame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frame = transform.Find("Frame");
        frame.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == GameState.GAME_COMBAT_END)
        {
            GameManager.instance.Pause(frame.gameObject);
        }
    }

    public void NextWave()
    {
        GameManager.instance.UnPause(frame.gameObject);
        GameManager.instance.SwitchState();
    }

}
