using FishNet.Object;
using FishNet.Connection;
using UnityEngine;

public class Player : NetworkBehaviour
{

    private PlayerMovement playerMovement;
    private PlayerPickupDrop playerPickupDrop;
    private Rigidbody2D rb;
    public float carryDistance = 0f; // Distance from the player where the object will be carried
    private GameObject carriedObject;


    public float spawnAreaWidth = 4f;
    public float spawnAreaHeight = 4f;

    public float spawnCheckRadius = 1f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerPickupDrop = GetComponent<PlayerPickupDrop>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (IsOwner)
        {
            // Position the player at a random location when they join
            // transform.position = GetRandomSpawnPosition();
            transform.position = GetRandomValidSpawnPosition();
            SetRandomColor();
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            playerMovement.HandleInput();
            playerMovement.RotateTowardsDirection();
            playerMovement.MovePlayer();
            playerPickupDrop.Update();
        }
    }

    private Vector2 GetRandomValidSpawnPosition()
    {
        Vector2 spawnPosition;
        int maxAttempts = 100; // Limit the number of attempts to find a valid position
        int attempt = 0;

        do
        {
            float x = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            float y = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
            spawnPosition = new Vector2(x, y);

            // Check if the position is valid (e.g., not colliding with obstacles)
            if (!IsPositionOccupied(spawnPosition))
            {
                return spawnPosition;
            }

            attempt++;
        } while (attempt < maxAttempts);

        // Fallback to a default position if no valid position is found
        return Vector2.zero;
    }

    private bool IsPositionOccupied(Vector2 position)
    {
        // Check for collisions at the given position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, spawnCheckRadius);
        return colliders.Length > 0;
    }


    private void SetRandomColor()
    {
        if (spriteRenderer != null)
        {
            // Generate a random color
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Assign the random color to the sprite
            spriteRenderer.color = randomColor;
        }
    }


}