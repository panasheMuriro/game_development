using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class PlateSpawner : NetworkBehaviour
{
    // [SerializeField] private GameObject platePrefab;
    public GameObject platePrefab;

    // private GameObject currentPlate;


    private void Start()
    {
        if (IsServer)
        {
            SpawnPlate();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlate()
    {
        GameObject currentPlate = Instantiate(platePrefab, transform.position, transform.rotation);
        Spawn(currentPlate);
    }


    // PlateSpawner Dirty plate
    // [ServerRpc(RequireOwnership = false)]
    // public void ()
    // {
    //     SpawnPlate();
    // }

}
