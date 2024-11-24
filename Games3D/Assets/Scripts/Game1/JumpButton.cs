using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    public Rigidbody targetRigidbody; // The Rigidbody to apply the jump force to
    public float jumpForce = 5f;      // Force of the jump

    private void Start()
    {
        // Ensure the button has a Button component
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // Attach the Jump method to the button's onClick event
            button.onClick.AddListener(Jump);
        }
        else
        {
            Debug.LogError("No Button component found! Attach this script to a UI Button.");
        }
    }

    private void Jump()
    {
        if (targetRigidbody == null)
        {
            Debug.LogError("Target Rigidbody is not assigned!");
            return;
        }

        // Apply an upward force to the Rigidbody
        targetRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
