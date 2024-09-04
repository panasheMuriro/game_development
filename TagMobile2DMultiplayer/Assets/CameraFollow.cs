// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     public Transform player; // Reference to the player's Transform
//     public float smoothSpeed = 0.125f; // How smooth the camera follows the player
//     public Vector3 offset; // Offset from the player's position to the camera's position

//     private Camera cameraComponent;
//     private float halfWidth;
//     private float halfHeight;

//     void Start()
//     {
//         // Get the camera component
//         cameraComponent = GetComponent<Camera>();

//         // Calculate the half width and half height of the camera view
//         halfWidth = cameraComponent.orthographicSize * cameraComponent.aspect;
//         halfHeight = cameraComponent.orthographicSize;
//     }

//     void LateUpdate()
//     {
//         if (player == null)
//             return;

//         // Calculate the target position of the camera
//         Vector3 targetPosition = player.position + offset;

//         // Clamp the target position to prevent the player from moving off-screen
//         targetPosition.x = Mathf.Clamp(targetPosition.x, -halfWidth, halfWidth);
//         targetPosition.y = Mathf.Clamp(targetPosition.y, -halfHeight, halfHeight);

//         // Smoothly move the camera towards the target position
//         Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
//         transform.position = smoothedPosition;
//     }
// }
