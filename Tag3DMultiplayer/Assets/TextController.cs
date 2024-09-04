using UnityEngine;
using UnityEngine.UI;

using TMPro;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class TextController :  MonoBehaviour
{
// public static TaggedManager instance;

// Reference to the UI Text component
// private bool isTextVisible = true;

// public TextMeshProUGUI taggedText;

// public readonly SyncVar<string> text = new SyncVar<string>();



// void Awake()
// {
//     text.OnChange += OnTextChanged;
// }



// private void OnTextChanged(string oldValue, string newValue, bool asServer)
// {
//     // Update the score display when it changes
//     ToggleText();
// }

// [ServerRpc(RequireOwnership = false)]

public void ClearText(){
    TaggedManager.instance.HideTagged();
}

// This method toggles the visibility of the text
// public void ToggleText()
// {

//     Debug.Log("This");
//     if (taggedText != null)
//     {
//         Debug.Log("This");
//         isTextVisible = !isTextVisible;
//         taggedText.enabled = isTextVisible;
//     }
// }

// This method changes the text content
// public void ChangeText(string newText)
// {
//     if (taggedText != null)
//     {
//         taggedText.text = newText;
//     }
// }
}
