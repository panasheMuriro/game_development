using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Reference to the player (ball)
    public Vector3 offset = new Vector3(0, 5, -10); // Camera position offset

    void Update()
    {
        // Follow the player with an offset
        transform.position = player.position + offset;
        transform.LookAt(player); // Ensure the camera is looking at the player
    }
}
