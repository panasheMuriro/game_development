using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // Sensitivity of tilt-based movement

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get accelerometer input
        Vector3 tilt = Input.acceleration;

        // Map tilt to movement velocity
        Vector3 movement = new Vector3(tilt.x, 0, tilt.y) * speed;

        // Apply the movement to the ball
        rb.velocity = movement;

        // Debugging: Log tilt values
        Debug.Log($"Tilt: X={tilt.x}, Y={tilt.y}");
    }
}
