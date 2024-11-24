using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject XLine;           // Reference to the XLine GameObject
    public GameObject ZLine;           // Reference to the ZLine GameObject

    // Movement settings for XLine
    public float xStart = -6.7f;       // Starting X position for XLine
    public float xEnd = 5.8f;          // Ending X position for XLine
    public float xSpeed = 2.0f;        // Speed for XLine
    private bool movingXToEnd = true;  // Direction of XLine movement

    // Movement settings for ZLine
    public float zStart = -5.7f;       // Starting Z position for ZLine
    public float zEnd = 4.6f;          // Ending Z position for ZLine
    public float zSpeed = 3.5f;        // Speed for ZLine
    private bool movingZToEnd = true;  // Direction of ZLine movement

    private void Update()
    {
        MoveXLine();
        MoveZLine();
    }

    private void MoveXLine()
    {
        if (XLine == null) return;

        // Get current position
        Vector3 currentPosition = XLine.transform.position;

        // Determine the target X position
        float targetX = movingXToEnd ? xEnd : xStart;

        // Move towards the target position
        float newX = Mathf.MoveTowards(currentPosition.x, targetX, xSpeed * Time.deltaTime);

        // Update XLine's position
        XLine.transform.position = new Vector3(newX, currentPosition.y, currentPosition.z);

        // Reverse direction if target is reached
        if (Mathf.Approximately(newX, targetX))
        {
            movingXToEnd = !movingXToEnd;
        }
    }

    private void MoveZLine()
    {
        if (ZLine == null) return;

        // Get current position
        Vector3 currentPosition = ZLine.transform.position;

        // Determine the target Z position
        float targetZ = movingZToEnd ? zEnd : zStart;

        // Move towards the target position
        float newZ = Mathf.MoveTowards(currentPosition.z, targetZ, zSpeed * Time.deltaTime);

        // Update ZLine's position
        ZLine.transform.position = new Vector3(currentPosition.x, currentPosition.y, newZ);

        // Reverse direction if target is reached
        if (Mathf.Approximately(newZ, targetZ))
        {
            movingZToEnd = !movingZToEnd;
        }
    }
}
