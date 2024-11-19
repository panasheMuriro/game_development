using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject spherePrefab;  // Reference to the sphere prefab
    public Camera mainCamera;        // Reference to the main camera (drag it in the inspector)

    void Update()
    {

        if (Input.GetMouseButtonDown(0))  // 0 is for left mouse button (or screen touch)
        {
            // Cast a ray from the camera to the clicked position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast and check if it hits an object
            if (Physics.Raycast(ray, out hit))
            {
                // Instantiate the sphere at the point where the ray hit the surface
                GameObject sphere = Instantiate(spherePrefab, hit.point, Quaternion.identity);

                // Get the Renderer component of the sphere
                Renderer sphereRenderer = sphere.GetComponent<Renderer>();

                // Apply a random color to the sphere
                sphereRenderer.material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }
}
