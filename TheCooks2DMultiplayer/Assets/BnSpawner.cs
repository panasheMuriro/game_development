using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class BnSpawner : NetworkBehaviour
{
    // [SerializeField] private GameObject bnPrefab;
    public GameObject bnPrefab;

    // private GameObject currentBn;


    private void Start()
    {
        if (IsServer)
        {
            SpawnBn();
        }
    }

    private void SpawnBn()
    {
        GameObject currentBn = Instantiate(bnPrefab, transform.position, transform.rotation);
        Spawn(currentBn);
    }

    [ServerRpc(RequireOwnership = false)]
    public void BnPickedUp()
    {
        SpawnBn();
    }

}
