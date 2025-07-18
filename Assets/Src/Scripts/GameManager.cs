using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int gameScore = 0;

    [SerializeField] TextMeshProUGUI scoreText;


    public Transform player;

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

<<<<<<< Updated upstream
=======
    public void OnPlayerHit(float damageAmount)
    {
        float amount = (playerHealth - damageAmount);
        SetHealth(amount);
    }


    void SetHealth(float amount)
    {
        playerHealth = amount;
        playerHealth = Mathf.Clamp(playerHealth, 0f, 1f);
        GameManager.instance.healthBar.UpdateBar01(playerHealth);
    }

>>>>>>> Stashed changes
}

