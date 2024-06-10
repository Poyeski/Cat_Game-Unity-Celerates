using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text scoreText;
    public TMP_Text timerText;
    public int targetScore = 100;
    public float timeLimit = 60f;
    public int penaltyPoints = 5;
    public string NextSceneName;
    public string ReturnSceneName;

    private int currentScore;
    private float timeLeft;
    private bool gameEnded;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentScore = 0;
        timeLeft = timeLimit;
        gameEnded = false;
        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        if (gameEnded) return;

        timeLeft -= Time.deltaTime;
        UpdateTimerText();
        if (timeLeft <= 0)
        {
            EndGame();
        }
    }

    public void AddScore(int points)
    {
        if (gameEnded) return;

        currentScore += points;
        UpdateScoreText();
    }

    public void MissFood()
    {
        if (gameEnded) return;

        currentScore -= penaltyPoints;
        if (currentScore < 0)
        {
            currentScore = 0;
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Round(timeLeft);
    }

    void EndGame()
    {
        gameEnded = true;
        if (currentScore >= targetScore)
        {
            if (!string.IsNullOrEmpty(NextSceneName))
            {
                SceneManager.LoadScene(NextSceneName);
            }
            else
            {
                Debug.LogError("Return scene name is not specified.");
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(ReturnSceneName))
            {
                SceneManager.LoadScene(ReturnSceneName);
            }
        }
    }
}
