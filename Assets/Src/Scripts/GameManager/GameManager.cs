using TMPro;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


public enum GameState
{
    GAME_START,
    GAME_COMBAT,
    GAME_COMBAT_END,
    GAME_PAUSE,
}

public enum Upgrades
{
    MAX_HP,
    SPEED,
    DAMAGE,
    CRIT,
    PEIRCE,
    FIRE_RATE,
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static int experience = 0;

    public static int wave = 0;

    public static int money = 0;

    public static int musicVolume = 50;

    public static int SFXVolume = 50;

    public static GameState gameState = GameState.GAME_START;

    public static string startGunName = "Gun_Pistol";

    public static bool gameIsPause { get; private set; } = false;

    public static int playerHealth = 1;



    [Header("Player Stats")]

    public Transform player;

    [Header("Enemies")]

    public Transform[] enemies;

    [Header("DropItems")]

    public Transform coin;

    public Transform[] pistolUpgrades;

    public Transform[] guns;


    [Header("Bullets")]

    public Transform normalBullet;

    public Transform fireBullet;

    public Transform lightingBullet;

    public Transform iceBullet;

    public Transform poisonBullet;

    public Transform bombBullet;

    [Header("Effects")]

    public GameObject chainLightingEffect;

    public GameObject beenStruck;

    [Header("Debug")]
    public bool playOnAwake;

    EnemySpawner enemySpawner = null;

    public float combatTime { get; private set; } = 100f;

    public float timer { get; private set; } = 0;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        if (playOnAwake) gameState = GameState.GAME_COMBAT;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (gameState == GameState.GAME_START)
        {
            wave = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }


        if (TryGetComponent<EnemySpawner>(out enemySpawner))
        {
            if (gameState == GameState.GAME_COMBAT)
            {
                enemySpawner.enabled = true; // Note test code
                enemySpawner.SetUp();
                // player.GetComponent<PlayerGunController>().SetGun(startGunName);
            }
            else
            {
                enemySpawner.enabled = false;
            }
                
        }

    }

    private void Update()
    {
        if (gameState == GameState.GAME_COMBAT)
        {
            timer += Time.deltaTime;
            if (timer > combatTime) SwitchState();
        }

        if (gameState  == GameState.GAME_COMBAT_END)
        {

        }
    }

    public void SwitchState()
    {
        if (gameState == GameState.GAME_COMBAT_END)
        {
            wave++;
            timer = 0;
            enemySpawner.enabled = true;
            enemySpawner.SetUp();
            gameState = GameState.GAME_COMBAT;
        }
        else if (gameState == GameState.GAME_COMBAT)
        {
            enemySpawner.enabled = false;
            gameState = GameState.GAME_COMBAT_END;
        }
    }

    public static void AddScore(TextMeshProUGUI scoreText)
    {

    }
  
    //public void OnPlayerHit(float damageAmount)
    //{
    //    float amount = (playerHealth - damageAmount);
    //    playerHealth = amount;
    //    playerHealth = Mathf.Clamp(playerHealth, 0f, 1f);

    //    //if (healthBar.TryGetComponent<MMProgressBar>(out MMProgressBar bar))
    //    //{
    //    //    bar.UpdateBar01(playerHealth);
    //    //}
    //}

    public static void Pause(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;

        gameIsPause = true;
    }

    public static void UnPause(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;

        gameIsPause = false;
    }

    //public void SwitchWeapon(int index)
    //{
    //    PlayerGunController gunController = player.GetComponent<PlayerGunController>();
    //    switch (index)
    //    {
    //        case 0:
    //            {
    //                gunController.SetGun("Gun_Pistol");
    //                break;
    //            }
    //        case 1:
    //            {
    //                gunController.SetGun("Gun_AR");
    //                break;
    //            }
    //        case 2:
    //            {
    //                gunController.SetGun("Gun_Magnum");
    //                break;
    //            }
    //        case 3:
    //            {
    //                gunController.SetGun("Gun_SMG");
    //                break;
    //            }
    //        case 4:
    //            {
    //                gunController.SetGun("Gun_Sniper");
    //                break;
    //            }
    //        case 5:
    //            {
    //                gunController.SetGun("Gun_Gatling");
    //                break;
    //            }
    //    }

    //}

    public void LoadPreGameScene()
    {
        gameState = GameState.GAME_START;
        SceneManager.LoadScene(1);
    }

    public void LoadGameScene()
    {
        gameState = GameState.GAME_COMBAT;
        SceneManager.LoadScene(2);
    }

    public void LoadStartMenu()
    {
        gameState = GameState.GAME_START;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        Application.Quit();
    }

}

