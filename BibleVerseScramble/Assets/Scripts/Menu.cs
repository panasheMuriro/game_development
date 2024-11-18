using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    // Reference to the TextMeshPro component
    public TMP_Text itemText;

    // Add a method that will be called on mouse down
    void OnMouseDown()
    {
        if (itemText != null)
        {
            // Get the text from the TMP_Text component
            string category = itemText.text;

            // Load the scene with the text as a parameter
            LoadSceneWithParam(category);
        }
    }

    // Load scene with the given scene name
    private void LoadSceneWithParam(string category)
    {
        // Optionally, pass data through a static method or PlayerPrefs to retrieve the text value
        PlayerPrefs.SetString("Category", category);
        SceneManager.LoadScene("Verses");
    }
}
