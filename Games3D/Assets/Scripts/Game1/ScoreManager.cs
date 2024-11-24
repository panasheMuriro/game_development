using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance for global access
    private int score = 200; // Initial score

    public TextMeshProUGUI scoreText; // Reference to the TMP UI element

    private void Awake()
    {
        // Singleton pattern to ensure a single instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText(); // Ensure the score is displayed at start
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        Debug.Log($"Score Added! New Score: {score}");
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score}";
        }
        else
        {
            Debug.LogError("Score Text not assigned in ScoreManager!");
        }
    }
}
