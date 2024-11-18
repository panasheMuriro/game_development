using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadSegmentPrefab;  // Road segment to instantiate
    public float segmentLength = 50f;     // Length of each road segment
    public float speed = 5f;              // Speed of the road

    public Transform player;             // Player reference
    private Vector3 lastRoadPosition;     // Last position where road was spawned

    void Start()
    {
        // player = GameObject.FindWithTag("Player").transform;
        lastRoadPosition = transform.position;
    }

    void Update()
    {
        // Move road segments forward
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // Check if we need to spawn a new road segment
        if (player.position.z > lastRoadPosition.z - segmentLength)
        {
            SpawnRoadSegment();
        }
    }

    void SpawnRoadSegment()
    {
        // Instantiate a new road segment
        Vector3 spawnPosition = lastRoadPosition + Vector3.forward * segmentLength;
        Instantiate(roadSegmentPrefab, spawnPosition, Quaternion.identity);

        // Update the last road position
        lastRoadPosition = spawnPosition;
    }
}
