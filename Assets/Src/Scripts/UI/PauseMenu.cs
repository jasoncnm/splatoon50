using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    bool _GameIsPause = false;

    Transform menuItems;

    private void Start()
    {
        menuItems = transform.Find("MenuItems");
        menuItems.gameObject.SetActive(false);
        GameManager.UnPause(menuItems.gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.gameState == GameState.GAME_COMBAT)
            {
                _GameIsPause = true;    
                GameManager.gameState = GameState.GAME_PAUSE;
                GameManager.Pause(menuItems.gameObject);
            }
            else if (GameManager.gameState == GameState.GAME_PAUSE)
            {
                _GameIsPause = false;
                GameManager.gameState = GameState.GAME_COMBAT;
                GameManager.UnPause(menuItems.gameObject);
            }
        }
    }
}
