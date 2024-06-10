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
    public string returnSceneName; 

    private int currentScore;
    private float timeLeft;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentScore = 0;
        timeLeft = timeLimit;
        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        UpdateTimerText();
        if (timeLeft <= 0)
        {
            EndGame();
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();
        if (currentScore >= targetScore)
        {
            EndGame();
        }
    }

    public void MissFood()
    {
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
        if (currentScore >= targetScore && timeLeft <= 0)
        {
            if (!string.IsNullOrEmpty(returnSceneName))
            {
                SceneManager.LoadScene(returnSceneName);
            }
            else
            {
                Debug.LogError("Return scene name is not specified.");
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(returnSceneName))
            {
                SceneManager.LoadScene(returnSceneName);
            }
            else
            {
                Debug.LogError("Return scene name is not specified.");
            }
        }
    }
}