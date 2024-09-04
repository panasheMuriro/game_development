using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Object.Synchronizing;

public class Pan : NetworkBehaviour
{
    // public bool isCooking { get; set; } = false;

    public readonly SyncVar<bool> isCooking = new SyncVar<bool>();

    [ServerRpc(RequireOwnership = false)]
    public void StartCooking(Meat meat)
    {
        if (!isCooking.Value && meat.isCut.Value)
        {
            Debug.Log("Should start cooking");
            StartCoroutine(CookMeat(meat));
        }
    }

    private IEnumerator CookMeat(Meat meat)
    {
        isCooking.Value = true;
        float cookingTime = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < cookingTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        meat.Cook();
        isCooking.Value = false;
    }

    
}
