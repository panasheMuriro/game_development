using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void ExitGame()
    {
        // Logs a message in the editor for testing
        Debug.Log("Game is exiting...");

        // Closes the application
        Application.Quit();

        // In the Unity Editor, the application doesn't actually quit,
        // so we can add this to simulate the behavior in the editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
