using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int scoreReductionValue = -10; // How much score to reduce on trigger


    private void OnTriggerEnter(Collider other)
    {
        // Reduce the score using the ScoreManager
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreReductionValue);
        }
        else
        {
            Debug.LogError("ScoreManager instance not found!");
        }

        Debug.Log($"Collided with: {other.name}");
    }
}
