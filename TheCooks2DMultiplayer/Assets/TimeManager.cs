using UnityEngine;
using TMPro;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class TimeManager : NetworkBehaviour
{
    public static TimeManager instance;

    // private const int initialTime = 120;

    public TextMeshProUGUI timeText;
    // private int score = 0;

    public readonly SyncVar<float> timeLeft = new SyncVar<float>(360f);


    void Awake()
    {
        // Ensure there is only one instance of the ScoreManager
        // score.OnChange += OnScoreChanged;

        if (instance == null)
        {
            instance = this;
            // Register the OnScoreChanged callback
            timeLeft.OnChange += OnTimeChanged;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateTimeText();
    }

    void Update()
    {
        if (timeLeft.Value > 0)
        {
            timeLeft.Value -= Time.deltaTime;
            UpdateTimeText();
        }

    }

    private void OnTimeChanged(float oldValue, float newValue, bool asServer)
    {
        // Update the score display when it changes
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        int ts = (int)timeLeft.Value;
        timeText.text = ts.ToString();
    }

}
