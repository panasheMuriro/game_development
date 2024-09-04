using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class RigidBodyMovement : NetworkBehaviour
{


    Vector3 pos;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.position);
        if (transform.position != pos)
        {
            SendPositionToServer(transform.position);
            pos = transform.position;
        }
    }





     [ServerRpc(RequireOwnership = false)]
    private void SendPositionToServer(Vector3 newPosition)
    {
        // Update position on the server
        transform.position = newPosition;

        // Broadcast the new position to all clients
        UpdatePositionForObservers(newPosition);
    }

    [ObserversRpc]
    private void UpdatePositionForObservers(Vector3 newPosition)
    {
        // Update position on all clients
        // transform.position = newPosition;

        // Debug.Log("Pos sent back " + newPosition);

        // rb.position = newPosition;


        rb.position = newPosition;

        // Optionally, update the transform position too, if necessary
        // transform.position = newPosition;
        Debug.Log("Position updated on observers: " + newPosition);
    }




}
