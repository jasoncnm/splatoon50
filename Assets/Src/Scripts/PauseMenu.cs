using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    bool _GameIsPause = false;

    Transform menuItems;

    private void Start()
    {
        menuItems = transform.Find("MenuItems");
        menuItems.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_GameIsPause)
            {
                _GameIsPause = true;
                GameManager.Pause(menuItems.gameObject);

            }
            else
            {
                _GameIsPause = false;
                GameManager.UnPause(menuItems.gameObject);
            }
        }
    }
}
