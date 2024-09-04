using UnityEngine;
using TMPro;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class TaggedManager : NetworkBehaviour
{
    public static TaggedManager instance;

    public TextMeshProUGUI taggedText;


    public readonly SyncVar<string> tagged = new SyncVar<string>();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Register the OnScoreChanged callback
            tagged.OnChange += OnTaggedChanged;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    void Start()
    {
        UpdateTaggedText();
    }


    [ServerRpc(RequireOwnership = false)]

    public void ShowTagged()
    {
        tagged.Value = "Tagged";
    }




    [ServerRpc(RequireOwnership = false)]

    public void HideTagged()
    {
        tagged.Value = "";
    }




    private void OnTaggedChanged(string oldValue, string newValue, bool asServer)
    {
        // Update the score display when it changes
        UpdateTaggedText();
    }

    private void UpdateTaggedText()
    {
        taggedText.text = tagged.Value;
    }
}
