using UnityEngine;

public class CapsuleColorChanger : MonoBehaviour
{
    public Color capsuleColor = Color.red;  // Set this to any color you like

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = capsuleColor;
        }
    }
}
