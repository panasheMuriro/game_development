using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class MeatSpawner : NetworkBehaviour
{
    public GameObject meatPrefab;

    public override void OnStartServer()
    {
        base.OnStartServer();
        SpawnMeat();
    }


    private void Start()
    {
        if (IsServer)
        {
            SpawnMeat();
        }
    }

    private void SpawnMeat()
    {
        GameObject currentMeat = Instantiate(meatPrefab, transform.position, transform.rotation);
        Spawn(currentMeat);
    }

    [ServerRpc(RequireOwnership = false)]
    public void MeatPickedUp()
    {
        SpawnMeat();
    }

}
