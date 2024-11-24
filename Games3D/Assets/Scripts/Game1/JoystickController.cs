using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody rb;
    public VariableJoystick variableJoystick;
    public float rotationSpeed = 5f; // Adjust rotation sensitivity

    private void Start()
    {
        // Ensure Joystick component is properly assigned
        if (variableJoystick == null)
        {
            variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        }
    }

    private void FixedUpdate()
    {
        // Get joystick input as a 2D direction
        Vector2 direction = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical);

        // If there's input from the joystick, move and rotate the player
        if (direction.sqrMagnitude > 0.01f)
        {
            // Convert 2D direction to 3D for movement on the X-Z plane
            Vector3 movement = new Vector3(direction.x, 0, direction.y).normalized * speed * Time.fixedDeltaTime;

            // Move the Rigidbody
            rb.MovePosition(rb.position + movement);

            // Rotate the player to face the joystick direction
            Vector3 lookDirection = new Vector3(direction.x, 0, direction.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
