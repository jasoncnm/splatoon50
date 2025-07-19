using TMPro;
using UnityEngine;
using MoreMountains.Tools;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static int gameScore = 0;

    public float playerHealth = 1f;

    public Transform player;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScore = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

    }

    public static void AddScore(TextMeshProUGUI scoreText)
    {
        gameScore++;
        scoreText.text = gameScore.ToString();
    }
  
    public void OnPlayerHit(float damageAmount)
    {
        float amount = (playerHealth - damageAmount);
        playerHealth = amount;
        playerHealth = Mathf.Clamp(playerHealth, 0f, 1f);

        //if (healthBar.TryGetComponent<MMProgressBar>(out MMProgressBar bar))
        //{
        //    bar.UpdateBar01(playerHealth);
        //}
    }

    public static void Pause(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public static void UnPause(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void SwitchWeapon(int index)
    {
        PlayerGunController gunController = player.GetComponent<PlayerGunController>();
        switch (index)
        {
            case 0:
                {
                    gunController.SetGun("Gun_Pistol");
                    break;
                }
            case 1:
                {
                    gunController.SetGun("Gun_AR");
                    break;
                }
            case 2:
                {
                    gunController.SetGun("Gun_Magnum");
                    break;
                }
            case 3:
                {
                    gunController.SetGun("Gun_SMG");
                    break;
                }
            case 4:
                {
                    gunController.SetGun("Gun_Sniper");
                    break;
                }
            case 5:
                {
                    gunController.SetGun("Gun_Gatling");
                    break;
                }
        }

    }





}

