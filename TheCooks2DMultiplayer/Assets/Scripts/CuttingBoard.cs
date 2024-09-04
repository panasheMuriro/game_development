using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class CuttingBoard : NetworkBehaviour
{
    public readonly SyncVar<bool> isOccupied = new SyncVar<bool>(new SyncTypeSettings());

    [ServerRpc(RequireOwnership = false)]
    public void SetIsOccupied(bool value)
    {
        isOccupied.Value = value;
    }

    // public bool GetIsOccupied()
    // {
    //     return _isOccupied.Value;
    // }
}
