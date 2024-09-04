using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class PlayerMovement : NetworkBehaviour
{
    // 3.144.94.123
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    public float spawnAreaWidth = 4f;
    public float spawnAreaHeight = 4f;
    public float spawnAreaDepth = 4f; // Added depth for 3

    public float spawnCheckRadius = 1f;

    private Renderer renderer;


    // public TextMeshProUGUI scoreText;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    // }


    public override void OnStartClient()
    {
        base.OnStartClient();
        if (IsOwner)
        {
            // Position the player at a random location when they join
            transform.position = GetRandomValidSpawnPosition();
            SetRandomColor();
        }
    }


    void Update()
    {

        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (IsOwner)
        {
            // Move the player left and right
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
        }
    }





    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        // Check if the player is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("TAGGED");
            TaggedManager.instance.ShowTagged();
        }

    }


    private Vector3 GetRandomValidSpawnPosition()
    {
        Vector3 spawnPosition;
        int maxAttempts = 100; // Limit the number of attempts to find a valid position
        int attempt = 0;

        do
        {
            float x = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            float y = Random.Range(0, spawnAreaHeight); // Adjust y to be positive (height)
            float z = Random.Range(-spawnAreaDepth / 2, spawnAreaDepth / 2);
            spawnPosition = new Vector3(x, y, z);

            // Check if the position is valid (e.g., not colliding with obstacles)
            if (!IsPositionOccupied(spawnPosition))
            {
                return spawnPosition;
            }

            attempt++;
        } while (attempt < maxAttempts);

        // Fallback to a default position if no valid position is found
        return Vector3.zero;
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        // Check for collisions at the given position
        Collider[] colliders = Physics.OverlapSphere(position, spawnCheckRadius); // Use Physics.OverlapSphere for 3D
        return colliders.Length > 0;
    }


    private void SetRandomColor()
    {
        if (GetComponent<Renderer>() != null)
        {
            // Generate a random color
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Assign the random color to the material
            GetComponent<Renderer>().material.color = randomColor;
        }
    }
}
