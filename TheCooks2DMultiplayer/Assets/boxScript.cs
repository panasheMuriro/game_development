using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using WebSocketSharp;



public class boxScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 5f;

    // private WebSocket websocket;


    void Start()
    {

        // websocket = new WebSocket("ws://localhost:3000");

        // websocket.OnOpen += (sender, e) =>
        // {
        //     Debug.Log("Connected to server");
        // };

        // websocket.OnError += (sender, e) =>
        // {
        //     Debug.Log("Error! " + e.Message);
        // };

        // websocket.OnClose += (sender, e) =>
        // {
        //     Debug.Log("Disconnected from server");
        // };

        // websocket.OnMessage += (sender, e) =>
        // {
        //     Debug.Log("Received message from server: " + e.Data);
        // };

        // websocket.Connect();

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        // Calculate the new position
        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        // Apply the movement to the object
        // if (move != Vector3.zero)
        // {
        //     // Debug.Log(move.ToString());
        //     websocket.Send(move.ToString());
        // }

        transform.Translate(move);


    }
}
