using TMPro;
using UnityEngine;
using MoreMountains.Tools;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] TextMeshProUGUI scoreText;

    public MMProgressBar healthBar;

    public int gameScore = 0;
    
    public Transform player;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void AddScore()
    {
        gameScore++;
        scoreText.text = gameScore.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            healthBar.Plus10Percent();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            healthBar.Minus10Percent();
        }
    }

}

