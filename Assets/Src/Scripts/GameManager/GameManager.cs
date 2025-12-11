using TMPro;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


public enum Upgrades
{
    MAX_HP,
    SPEED,
    DAMAGE,
    CRIT,
    PEIRCE,
    FIRE_RATE,
}

public enum GameState
{
    GAME_START,
    GAME_COMBAT,
    GAME_COMBAT_END,
    GAME_PAUSE,
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set;  }
    public static GameState gameState = GameState.GAME_START;

    public static int experience = 0;
    public static int money = 0;
    public static int musicVolume = 50;
    public static int SFXVolume = 50;
    public static int playerHealth = 1;
    public static string startGunName = "Gun_Magnum";
    public bool gameIsPause { get; private set; } = false;
    public float combatTime = 50f;
    public int wave = 0;


    [Header("Player Stats")]
    public Transform player;

    [Header("Enemies")]

    public Transform[] enemies;

    [Header("DropItems")]

    public Transform coin;

    public Transform[] pistolUpgrades;

    public Transform[] guns;

    [Header("Chest")]
    public Transform chest;

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


    EnemySpawner enemySpawner = null;

    public float timer { get; private set; } = 0;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            gameState = GameState.GAME_COMBAT;
        }

        if (gameState == GameState.GAME_START)
        {
            InitGame();
        }
        else if (gameState == GameState.GAME_COMBAT)
        {
            InitGameplay();
        }
    }
    
    private void Update()
    {

        if (gameState == GameState.GAME_COMBAT)
        {
            timer += Time.deltaTime;
            if (timer > combatTime) SwitchState();
        }

        if (player && player.GetComponent<PlayerController>().GetHealth() <= 0)
        {
            enemySpawner.enabled = false;
            LoadStartMenu();
        }


        if (gameState == GameState.GAME_COMBAT_END)
        {

        }
    }

    void InitGame()
    {
        wave = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameState = GameState.GAME_START;
    }
    
    void InitGameplay()
    {

        player = GameObject.Find("Player").transform;

        if (TryGetComponent<EnemySpawner>(out enemySpawner))
        {
            if (gameState == GameState.GAME_COMBAT)
            {
                enemySpawner.enabled = true; // Note test code
                enemySpawner.SetUp();
            }
            else
            {
                enemySpawner.enabled = false;
            }

        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
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
            // Spawn chest
            Transform item = Instantiate(chest, player.transform.position, Quaternion.identity);
        }
        else if (gameState == GameState.GAME_COMBAT)
        {
            enemySpawner.enabled = false;
            gameState = GameState.GAME_COMBAT_END;
        }
    }

    public void AddScore(TextMeshProUGUI scoreText)
    {

    }


    public void Pause(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;

        gameIsPause = true;
    }

    public void UnPause(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;

        gameIsPause = false;
    }


    public void GameOver()
    {
        Application.Quit();
    }

    public void LoadPreGameScene()
    {
        GameManager.gameState = GameState.GAME_START;
        SceneManager.LoadScene(1);
    }

    public void LoadGameScene()
    {
        GameManager.gameState = GameState.GAME_COMBAT;
        SceneManager.LoadScene(2);
    }

    public void LoadStartMenu()
    {
        GameManager.gameState = GameState.GAME_START;
        SceneManager.LoadScene(0);
    }
}

