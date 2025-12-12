using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GlobalGameStateSO gameGlobal;

    bool _GameIsPause = false;

    Transform menuItems;
    GameManager gm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
        menuItems = transform.Find("MenuItems");
        menuItems.gameObject.SetActive(false);
        gm.UnPause(menuItems.gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameGlobal.gameState == GameState.GAME_COMBAT)
            {
                _GameIsPause = true;
                gameGlobal.gameState = GameState.GAME_PAUSE;
                gm.Pause(menuItems.gameObject);
            }
            else if (gameGlobal.gameState == GameState.GAME_PAUSE)
            {
                _GameIsPause = false;
                gameGlobal.gameState = GameState.GAME_COMBAT;
                gm.UnPause(menuItems.gameObject);
            }
        }
    }
}
