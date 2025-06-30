using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void Update()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene == 0)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
            }
        }
        else if (scene == 1)
        {
            if (GameManager.instance.playerHealth == 0)
            {
                SceneManager.LoadScene(2);
            }
        }
        else if (scene == 2)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }

        }
    }
}
