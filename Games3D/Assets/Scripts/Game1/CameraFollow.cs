using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // The player's transform
    public float smoothSpeed = 0.125f;  // Smooth transition speed
    public Vector3 offset = new Vector3(0, 5, -10); // Camera offset
    public float safeZone = 0.3f;  // How far the player can move in the viewport before the camera adjusts (0.3 = 30%)

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;  // Get the main camera
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Convert the player's position to viewport space
        Vector3 playerViewportPos = cam.WorldToViewportPoint(player.position);

        // Check if the player is within the defined "safe zone" in the viewport
        if (playerViewportPos.x < safeZone || playerViewportPos.x > 1 - safeZone ||
            playerViewportPos.y < safeZone || playerViewportPos.y > 1 - safeZone)
        {
            // Calculate the desired position with the offset
            Vector3 desiredPosition = player.position + offset;

            // Smoothly interpolate to the new position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}
