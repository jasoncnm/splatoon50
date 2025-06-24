using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int gameScore = 0;

    [SerializeField] TextMeshProUGUI scoreText;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void AddScore()
    {
        gameScore++;
        scoreText.text = gameScore.ToString();
    }

}

