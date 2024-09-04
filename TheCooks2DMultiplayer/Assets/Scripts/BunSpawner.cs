using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class BunSpawner : NetworkBehaviour
{
    // [SerializeField] private GameObject bunPrefab;
    public GameObject bunPrefab;

    // private GameObject currentBun;

    public override void OnStartServer()
    {
        base.OnStartServer();
        SpawnBun();
    }


    private void Start()
    {

    }

    private void SpawnBun()
    {
        GameObject currentBun = Instantiate(bunPrefab, transform.position, transform.rotation);
        Spawn(currentBun);
    }

    [ServerRpc(RequireOwnership = false)]
    public void BunPickedUp()
    {
        // if (currentBun != null)
        // {
        //     currentBun.GetComponent<NetworkObject>().Despawn();
        //     currentBun = null;
        // }
        SpawnBun();
    }

}
