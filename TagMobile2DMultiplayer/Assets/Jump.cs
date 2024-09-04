using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Events;
// using FishNet.Object;
// using FishNet.Connection;
// using FishNet.Object.Synchronizing;

public class Jump : MonoBehaviour
{

    public float jumpForce = 5f;

    private Rigidbody2D rb;
    public GameObject player;

    private Rigidbody2D playerRB;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRB = player.GetComponent<Rigidbody2D>();

    }


    //  public UnityEvent onClick;  // Event to trigger when the button is clicked

    public void OnMouseDown()
    {
        // This function is called when the square sprite is clicked
        Debug.Log("Hello World");
        JumpPlayer();
    }

    // // Start is called before the first frame update
    // public float jumpForce = 2f; // Adjust the jump force as needed

    // private Rigidbody2D rb;
    // public GameObject player;

    // private Rigidbody2D playerRB;


    // void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     playerRB = player.GetComponent<Rigidbody2D>();

    // }

    // void Update()
    // {
    //     // Handle mouse input for desktop
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         HandleInput(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //     }

    //     // Handle touch input for mobile
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);
    //         Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
    //         if (touch.phase == TouchPhase.Began)
    //         {
    //             HandleInput(touchPosition);
    //         }
    //     }


    // }

    // private void HandleInput(Vector2 inputPosition)
    // {
    //     // Check if the input position is within the bounds of the collider
    //     if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(inputPosition))
    //     {
    //         JumpPlayer();
    //     }
    // }

    // [ServerRpc(RequireOwnership = false)]
    private void JumpPlayer()
    {
        // Apply a force to the Rigidbody2D to make it jump
        playerRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }


}
