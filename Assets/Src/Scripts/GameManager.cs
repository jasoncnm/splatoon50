using TMPro;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.Rendering.PostProcessing;


public enum GameState
{
    GAME_START,
    GAME_COMBAT,
    GAME_COMBAT_END,
}

public class GameManager : MonoBehaviour
{


    public static GameManager instance;

    public static int gameScore = 0;

    public static int wave = 0;

    public static GameState gameState = GameState.GAME_START;

    public static bool gameIsPause { get; private set; } = false;

    [Header("Player Stats")]

    public float playerHealth = 1f;

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

    [Header("Effects")]

    public GameObject chainLightingEffect;

    public GameObject beenStruck;

    EnemySpawner enemySpawner = null;

    public float combatTime { get; private set; } = 120f;

    public float timer { get; private set; } = 0;

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
        gameState = GameState.GAME_COMBAT;
        enemySpawner = GetComponent<EnemySpawner>();

        enemySpawner.enabled = false;
        
        if (gameState == GameState.GAME_COMBAT)
            enemySpawner.enabled = true; // Note test codee
    }

    private void Update()
    {
        if (gameState == GameState.GAME_COMBAT)
        {
            timer += Time.deltaTime;
            if (timer > combatTime) SwitchState();
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

        gameIsPause = true;
    }

    public static void UnPause(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;

        gameIsPause = false;
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

