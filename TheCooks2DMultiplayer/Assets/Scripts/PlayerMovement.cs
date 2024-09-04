using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f; // Degrees per second
    private Rigidbody2D rb;
    private Vector2 inputDirection;
    private Vector2 targetDirection;
    private bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void HandleInput()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector2(moveHorizontal, moveVertical).normalized;

        if (inputDirection != Vector2.zero)
        {
            targetDirection = inputDirection;
        }
    }

    public void RotateTowardsDirection()
    {
        if (targetDirection == Vector2.zero)
            return;

        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;

        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
        float angleChange = Mathf.Sign(angleDifference) * rotationSpeed * Time.deltaTime;

        if (Mathf.Abs(angleChange) > Mathf.Abs(angleDifference))
        {
            angleChange = angleDifference;
        }

        transform.Rotate(0, 0, angleChange);

        if (Mathf.Approximately(Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle), 0))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public void MovePlayer()
    {
        if (isMoving && inputDirection != Vector2.zero)
        {
            Vector2 movement = inputDirection * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
    }
}
