// using UnityEngine;

// public class Draggable : MonoBehaviour
// {
//     private Vector3 offset;
//     private Camera mainCamera;

//     void Start()
//     {
//         mainCamera = Camera.main; // Get the main camera
//     }

//     void OnMouseDown()
//     {
//         // Calculate the offset between the mouse position and the object's position
//         offset = transform.position - GetMouseWorldPos();
//     }

//     void OnMouseDrag()
//     {
//         // Update the object's position based on the mouse position
//         transform.position = GetMouseWorldPos() + offset;
//     }

//     private Vector3 GetMouseWorldPos()
//     {
//         // Get the mouse position in the world
//         Vector3 mouseScreenPos = Input.mousePosition;
//         mouseScreenPos.z = mainCamera.nearClipPlane; // Set this to the camera's near clip plane
//         return mainCamera.ScreenToWorldPoint(mouseScreenPos);
//     }
// }


using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private Vector3 originalScale;
    public float scaleUpFactor = 1.1f; // Scale up by 10%

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera
        originalScale = transform.localScale; // Store the original scale of the object
    }

    void OnMouseDown()
    {
        // Scale up the object slightly
        LeanTween.scale(gameObject, originalScale * scaleUpFactor, 0.1f);

        // Calculate the offset between the mouse position and the object's position
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        // Update the object's position based on the mouse position
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        // Scale back down to the original size
        LeanTween.scale(gameObject, originalScale, 0.1f);
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get the mouse position in the world
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.nearClipPlane; // Set this to the camera's near clip plane
        return mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }
}
