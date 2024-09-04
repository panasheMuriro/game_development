using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Object.Synchronizing;

public class Meat : NetworkBehaviour
{
    public readonly SyncVar<bool> isCooked = new SyncVar<bool>();
    public readonly SyncVar<bool> isCut = new SyncVar<bool>();

    public Sprite uncutSprite;
    public Sprite cutSprite;
    public Sprite cookedSprite;

    private SpriteRenderer spriteRenderer;

    public bool IsPlaced { get; set; } = false;

    private void Awake()
    {
        isCooked.OnChange += OnCookedChanged;
        isCut.OnChange += OnCutChanged;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // UpdateColor();
        UpdateSprite();
    }


    [ServerRpc(RequireOwnership = false)]
    public void Cut()
    {
        // if (IsServer)
        // {
        isCut.Value = true;
        // }
    }

    [ServerRpc(RequireOwnership = false)]
    public void Cook()
    {
        // if (IsServer)
        // {
        isCooked.Value = true;
        // }
    }

    private void OnCookedChanged(bool prev, bool next, bool asServer)
    {
        // UpdateColor();
        UpdateSprite();
    }

    private void OnCutChanged(bool prev, bool next, bool asServer)
    {
        // UpdateColor();
        UpdateSprite();
    }

    // [ServerRpc(RequireOwnership = false)]
    private void UpdateSprite()
    {
        // Set the appropriate sprite based on the cut and cooked status
        if (isCooked.Value)
        {
            spriteRenderer.sprite = cookedSprite;
        }
        else if (isCut.Value)
        {
            spriteRenderer.sprite = cutSprite;
        }
        else
        {
            spriteRenderer.sprite = uncutSprite;
        }




        // Adjust the scale of the sprite to fit the prefab dimensions
        // spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f); // Adjust this scale factor as needed
    }
}
