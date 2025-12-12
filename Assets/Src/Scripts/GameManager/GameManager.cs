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

    public GlobalGameStateSO gameGlobal;
    public DropItemsSO dropItems;


    public Transform player = null;

    [Header("Debug")]
    public bool playOnAwake;

    EnemySpawner enemySpawner = null;


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
            gameGlobal.timer += Time.deltaTime;
            if (gameGlobal.timer > gameGlobal.combatTime) SwitchState();
        }

        if (player && player.GetComponent<PlayerController>().GetHealth() <= 0)
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
        gameGlobal.wave = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameGlobal.gameState = GameState.GAME_START;
    }
    
    public void InitGameplay()
    {
    
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
            gameGlobal.wave++;
            gameGlobal.timer = 0;
            enemySpawner.enabled = true;
            enemySpawner.SetUp();
            gameGlobal.gameState = GameState.GAME_COMBAT;
            // Spawn chest
            Transform item = Instantiate(dropItems.chest, player.transform.position, Quaternion.identity);
        }
        else if (gameGlobal.gameState == GameState.GAME_COMBAT)
        {
            enemySpawner.enabled = false;
            gameGlobal.gameState = GameState.GAME_COMBAT_END;
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

        gameGlobal.gameIsPause = true;
    }

    public void UnPause(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;

        gameGlobal.gameIsPause = false;
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

