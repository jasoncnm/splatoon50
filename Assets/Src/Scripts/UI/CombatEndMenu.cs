using UnityEngine;

public class CombatEndMenu : MonoBehaviour
{
    public GlobalGameStateSO gameGlobal;
    Transform frame;

    GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
        frame = transform.Find("Frame");
        frame.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameGlobal.gameState == GameState.GAME_COMBAT_END)
        {

            gm.Pause(frame.gameObject);
        }
    }

    public void NextWave()
    {
        gm.UnPause(frame.gameObject);
        gm.SwitchState();
    }

}
