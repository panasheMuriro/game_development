using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FishNet.Object;
using FishNet.Connection;
using FishNet.Object.Synchronizing;

public class Plate : NetworkBehaviour
{
    // public bool hasBun { get; set; } = false;
    // public bool hasMeat { get; set; } = false;

    public readonly SyncVar<bool> hasBun = new SyncVar<bool>();

    public readonly SyncVar<bool> hasMeat = new SyncVar<bool>();


    // public Transform BunIconPosition;

    // Reference to the BunIcon prefab
    public GameObject BunIconPrefab;

    private GameObject instantiatedBunIcon;


    public GameObject MeatIconPrefab;

    private GameObject instantiatedMeatIcon;

    private void Awake()
    {
        // Listen for changes in hasBun
        hasBun.OnChange += OnBunChanged;
        hasMeat.OnChange += OnMeatChanged;
    }




    [ServerRpc(RequireOwnership = false)]
    public void SetHasBunServerRpc()
    {
        transform.GetComponent<Plate>().hasBun.Value = true;
    }


    [ServerRpc(RequireOwnership = false)]
    public void SetHasMeatServerRpc()
    {
        transform.GetComponent<Plate>().hasMeat.Value = true;
    }


    private void OnBunChanged(bool oldValue, bool newValue, bool asServer)
    {
        if (newValue) // If hasBun becomes true
        {
            InstantiateBunIcon();
        }
        else
        {
            // Optionally, you could handle the case when hasBun becomes false
            // For example, destroying the bun icon if it was previously instantiated
            if (instantiatedBunIcon != null)
            {
                Destroy(instantiatedBunIcon);
            }
        }
    }


    private void OnMeatChanged(bool oldValue, bool newValue, bool asServer)
    {
        if (newValue) // If hasBun becomes true
        {

            InstantiateMeatIcon();
        }
        else
        {
            // Optionally, you could handle the case when hasBun becomes false
            // For example, destroying the bun icon if it was previously instantiated
            if (instantiatedMeatIcon != null)
            {
                Destroy(instantiatedMeatIcon);
            }
        }
    }



    private void InstantiateBunIcon()
    {
        Transform bunIconPosition = transform.Find("BunIconPoint");

        if (bunIconPosition == null)
        {
            Debug.LogError("BunIconPosition not found in the Plate GameObject.");
            return;
        }

        // Instantiate the BunIconPrefab at the BunIconPosition
        instantiatedBunIcon = Instantiate(BunIconPrefab, bunIconPosition.position, bunIconPosition.rotation);

        // Make the instantiated BunIcon a child of the BunIconPosition to maintain its position relative to the Plate
        instantiatedBunIcon.transform.SetParent(bunIconPosition);
    }




    private void InstantiateMeatIcon()
    {
        Transform meatIconPosition = transform.Find("MeatIconPoint");

        if (meatIconPosition == null)
        {
            Debug.LogError("MeatIconPosition not found in the Plate GameObject.");
            return;
        }

        // Instantiate the BunIconPrefab at the BunIconPosition
        instantiatedMeatIcon = Instantiate(MeatIconPrefab, meatIconPosition.position, meatIconPosition.rotation);

        // Make the instantiated BunIcon a child of the BunIconPosition to maintain its position relative to the Plate
        instantiatedMeatIcon.transform.SetParent(meatIconPosition);
    }



}
