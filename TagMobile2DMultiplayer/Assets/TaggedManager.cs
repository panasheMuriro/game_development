using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Object.Synchronizing;

public class TaggedManager : NetworkBehaviour
{
    public GameObject tagged;

    public static TaggedManager instance;

    public readonly SyncVar<bool> taggedVisible = new SyncVar<bool>();

    void Awake()
    {
        // Register the OnScoreChanged callback
        if (instance == null)
        {
            instance = this;
            taggedVisible.OnChange += OnTaggedVisibilityChanged;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void OnTaggedVisibilityChanged(bool oldValue, bool newValue, bool asServer)
    {
        // Update the score display when it changes
        UpdateTaggedVisibility();
    }



    [ServerRpc(RequireOwnership = false)]
    public void ShowTagged()
    {
        taggedVisible.Value = true;
        UpdateTaggedVisibility();
    }


    [ServerRpc(RequireOwnership = false)]
    public void HideTagged()
    {
        taggedVisible.Value = false;
        UpdateTaggedVisibility();
    }


    public void UpdateTaggedVisibility()
    {
        GameObject gm = GameObject.FindWithTag("tagged");
        if (taggedVisible.Value)
        {
            if (gm == null)
            {
                Instantiate(tagged, new Vector2(0, 0), Quaternion.identity);
            }
        }
        else
        {
            Destroy(gm);
        }
    }

}
