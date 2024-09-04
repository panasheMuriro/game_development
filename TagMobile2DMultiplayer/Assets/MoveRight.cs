using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Events;

public class MoveRight : MonoBehaviour
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

    private void OnMouseDown()
    {
        // This function is called when the square sprite is clicked
        Debug.Log("Hello World");



        MovePlayer();

    }

    private void MovePlayer()
    {
        // Apply a force to the Rigidbody2D to make it jump
        // playerRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        // Vector2 newPosition = playerRB.position + Vector2.right * 0.1f;
        // playerRB.MovePosition(newPosition);

        playerRB.AddForce(new Vector2(2f, 0), ForceMode2D.Impulse);


    }

}
