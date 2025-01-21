using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //  get the rigidbdy, appply force to it upon keyboard press
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Should jump");
        };
    }
}
