using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VerseBuilder : MonoBehaviour
{
    public TextMeshProUGUI sentenceText; // Reference to the TextMeshPro UI component in the UI
    private string sentence = "";        // Current sentence being built

    // Use Collider2D for trigger events

    void Start()
    {
        StartBreathingAnimation();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger activated with " + other.name);

        // Check if the other object has a TextMeshPro component
        TextMeshProUGUI otherTextComponent = other.GetComponentInChildren<TextMeshProUGUI>();

        if (otherTextComponent != null)
        {
            // Get the text from the colliding object
            string word = otherTextComponent.text;
            // Append the word to the sentence
            sentence += word + " ";
            // Update the UI element to show the updated sentence
            sentenceText.text = sentence;

             StartBreathingAnimation();
        }

 // Scale the object down to zero before destroying it
        LeanTween.scale(other.gameObject, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            Destroy(other.gameObject);
        });

        // Destroy(other.gameObject);
    }


    private void StartBreathingAnimation()
    {
        // Reset the scale to normal before starting
        gameObject.transform.localScale = Vector3.one;

        // Animate scale to give a breathing effect
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(() =>
            {
                LeanTween.scale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutSine);
            });
    }


    public void ResetSentence()
    {
        sentence = "";
        sentenceText.text = "";
    }
}
