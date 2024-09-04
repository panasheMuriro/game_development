using UnityEngine;
using TMPro;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public readonly SyncVar<int> score = new SyncVar<int>();



    void Awake()
    {
        // Ensure there is only one instance of the ScoreManager
        if (instance == null)
        {
            instance = this;
            // Register the OnScoreChanged callback
            score.OnChange += OnScoreChanged;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }


    [ServerRpc(RequireOwnership = false)]
    public void IncreaseScore(int amount)
    {
        // Ensure only the server can increase the score
        Debug.Log("Updating the score");
        score.Value += amount;
        UpdateScoreText();
    }

    private void OnScoreChanged(int oldValue, int newValue, bool asServer)
    {
        // Update the score display when it changes
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.Value.ToString();
    }
}
