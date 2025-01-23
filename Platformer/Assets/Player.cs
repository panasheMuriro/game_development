using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

    //  get the rigidbdy, appply force to it upon keyboard press
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("tree");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * 2f, rb.velocity.y);
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = new Vector2(rb.velocity.x, 5f);
        };
    }
}
