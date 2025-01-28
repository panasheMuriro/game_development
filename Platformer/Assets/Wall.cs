using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject w; // The prefab to instantiate
    public float spacing = 0.5f; // Space between the objects

    // public Rigidbody2D rb;

    void Start()
    {
        for (int j = -4; j < 4; j++)
        {
            for (int i = -4; i < 5.5; i++)
            {
                int randomInt = Random.Range(-4, 6);
                if (i == randomInt)
                {
                    continue;
                }
                // Calculate the vertical position for each object
                Vector3 position = new Vector3(-j * 2f, i * spacing, 0);
                // Instantiate the object at the calculated position
                Instantiate(w, position, Quaternion.identity);
            }
        }
    }

    // update wall should move right to left

    // void Update(){

    //     Rigidbody2D rb = w.GetComponent<Rigidbody2D>();


    // }
}
