using TMPro;
using UnityEngine;
using MoreMountains.Tools;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] TextMeshProUGUI scoreText;

    public MMProgressBar healthBar;

    public int gameScore = 0;

    public float playerHealth = 1f;

    public Transform player;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

}

