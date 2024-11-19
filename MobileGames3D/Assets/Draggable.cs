using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Camera mainCamera;         // Camera to convert screen space to world space
    private Vector3 offset;            // The offset between the mouse position and the sphere's position
    private bool isDragging = false;   // To check if the sphere is being dragged

    void Start()
    {
        mainCamera = Camera.main;      // Get the main camera
    }

    void Update()
    {
        // Check for mouse or touch input
        if (Input.GetMouseButtonDown(0))  // Left click or touch input
        {
            // Create a ray from the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the current sphere
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)  // Check if the hit object is the sphere
                {
                    isDragging = true;
                    // Calculate the offset between the mouse position and the sphere's position
                    offset = transform.position - hit.point;
                }
            }
        }

        // While the mouse is held down, drag the sphere
        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast again to update the sphere's position
            if (Physics.Raycast(ray, out hit))
            {
                transform.position = hit.point + offset;  // Set the sphere's position
            }
        }

        // Release the sphere when the mouse button is released or touch ends
        if (Input.GetMouseButtonUp(0))  // Left click or touch release
        {
            isDragging = false;
        }
    }
}
