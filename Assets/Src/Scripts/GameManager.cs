using TMPro;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.UI;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] GameObject pauseMenu;


    public MMProgressBar healthBar;

    public int gameScore = 0;

    public float playerHealth = 1f;

    public Transform player;

    bool _GameIsPause = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        pauseMenu.SetActive(false);
        gameScore = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;


    }

    public void AddScore()
    {
        gameScore++;
        scoreText.text = gameScore.ToString();
    }

    public void OnPlayerHit()
    {
        float amount = (playerHealth - 0.1f);
        SetHealth(amount);
    }


    void SetHealth(float amount)
    {
        playerHealth = amount;
        playerHealth = Mathf.Clamp(playerHealth, 0f, 1f);
        GameManager.instance.healthBar.UpdateBar01(playerHealth);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_GameIsPause)
            {
                _GameIsPause = true;
                pauseMenu.SetActive(true);

                Time.timeScale = 0f;
                Cursor.visible = true;

            }
            else
            {
                _GameIsPause = false;
                pauseMenu.SetActive(false);

                Time.timeScale = 1f;
                Cursor.visible = false;
            }
        }
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

