using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include this if you're using TextMeshPro

public class WordScrambler : MonoBehaviour
{
    public GameObject objectPrefab; // The object to instantiate (should have a TextMeshPro component)
    public RectTransform panel;      // Reference to the Panel RectTransform

    public string verse = "I can do all things through Christ who strengthens me";

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Split the verse into words
        string[] words = verse.Split(' ');

        // Instantiate an object for each word
        foreach (string word in words)
        {
            // Generate a random position within the panel
            Vector2 randomPosition = new Vector2(
                Random.Range(-panel.rect.width / 2, panel.rect.width / 2),
                Random.Range(-panel.rect.height / 2, panel.rect.height / 2)
            );

            // Instantiate the object at the random position relative to the panel
            GameObject obj = Instantiate(objectPrefab, panel);
            obj.GetComponent<RectTransform>().anchoredPosition = randomPosition; // Set position in the Panel's local space

            // Set the text of the instantiated object (assuming it has a TextMeshPro component)
            TextMeshProUGUI textComponent = obj.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = word; // Set the word text
            }
        }
    }
}
