using NUnit.Framework;
using TMPro;
using UnityEngine;
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
    public static GameManager instance { get ; private set; }

    public GlobalGameStateSO gameGlobal;
    public DropItemsSO dropItems;

    public int wave = 0;
    public float timer = 0;

    public bool gameIsPause = false;

    [Header("Debug")]
    public bool playOnAwake;

    EnemySpawner enemySpawner = null;
    PlayerController playerController = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (playOnAwake)
        {
            gameGlobal.gameState = GameState.GAME_COMBAT;
        }
        if (gameGlobal.gameState == GameState.GAME_START)
        {
            InitGame();
        }
        else if (gameGlobal.gameState == GameState.GAME_COMBAT)
        {
            InitGameplay();
        }
    }
    
    private void Update()
    {

        if (gameGlobal.gameState == GameState.GAME_COMBAT)
        {
            timer += Time.deltaTime;
            if (timer > gameGlobal.combatTime) SwitchState();
        }

        if (playerController != null && playerController.GetHealth() <= 0)
        {
            enemySpawner.enabled = false;
            LoadStartMenu();
        }


        if (gameGlobal.gameState == GameState.GAME_COMBAT_END)
        {

        }
    }

    void InitGame()
    {
        wave = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameGlobal.gameState = GameState.GAME_START;
    }
    
    public void InitGameplay()
    {
        
        if (playerController == null) playerController = FindAnyObjectByType<PlayerController>();
        Assert.IsTrue(playerController != null, "Failed to find player controller component!");

        if (TryGetComponent<EnemySpawner>(out enemySpawner))
        {
            if (gameGlobal.gameState == GameState.GAME_COMBAT)
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
        if (gameGlobal.gameState == GameState.GAME_COMBAT_END)
        {
            wave++;
            timer = 0;
            enemySpawner.enabled = true;
            enemySpawner.SetUp();
            gameGlobal.gameState = GameState.GAME_COMBAT;
            // Spawn chest
            Transform item = Instantiate(dropItems.chest, playerController.transform.position, Quaternion.identity);
        }
        else if (gameGlobal.gameState == GameState.GAME_COMBAT)
        {
            enemySpawner.enabled = false;
            gameGlobal.gameState = GameState.GAME_COMBAT_END;
        }
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
        gameGlobal.gameState = GameState.GAME_START;
        SceneManager.LoadScene(1);
    }

    public void LoadGameScene()
    {
        gameGlobal.gameState = GameState.GAME_COMBAT;
        SceneManager.LoadScene(2);
    }

    public void LoadStartMenu()
    {
        gameGlobal.gameState = GameState.GAME_START;
        SceneManager.LoadScene(0);
    }
}

