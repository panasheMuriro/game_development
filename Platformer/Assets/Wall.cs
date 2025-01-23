using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject w; // The prefab to instantiate
    public float spacing = 0.5f; // Space between the objects

    void Start()
    {
        for (int i = -8; i < 8; i++)
        {
            if (i == 0){
                continue;
            }
            // Calculate the vertical position for each object
            Vector3 position = new Vector3(-2, i * spacing, 0);
            // Instantiate the object at the calculated position
            Instantiate(w, position, Quaternion.identity);
        }
    }
}
