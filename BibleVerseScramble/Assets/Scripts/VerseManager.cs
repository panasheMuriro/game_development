using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VerseManager : MonoBehaviour
{
    public GameObject wordPrefab;
    public RectTransform panel;
    public TextMeshProUGUI sentenceText;
    public Button nextButton;

    public VerseBuilder verseBuilder;
    private string[] verses = {
        "I can do all things through Christ who strengthens me",
        "The joy of the Lord is my strength",
        "This is the day that the Lord has made; let us rejoice and be glad in it",
        "For I know the plans I have for you, declares the Lord",
        "You are fearfully and wonderfully made",
        "The Lord is my shepherd; I shall not want",
        "I am the light of the world",
        "Cast all your anxiety on Him because He cares for you",
        "And we know that in all things God works for the good of those who love Him",
        "Be strong and courageous. Do not be afraid; do not be discouraged",
        "Delight yourself in the Lord, and He will give you the desires of your heart",
        "Trust in the Lord with all your heart",
        "God is our refuge and strength, an ever-present help in trouble",
        "With God all things are possible",
        "No weapon formed against you shall prosper",
        "The Lord will fight for you; you need only to be still",
        "The peace of God, which transcends all understanding, will guard your hearts",
        "He will never leave you nor forsake you",
        "Love is patient, love is kind. It does not envy, it does not boast",
        "I can do everything through Him who gives me strength"
    };

    private int currentVerseIndex = 0;


    void Start()
    {
        ShuffleVerses();
        nextButton.onClick.AddListener(CheckAnswer);
        SpawnObjects();
    }


    void ShuffleVerses()
    {
        for (int i = 0; i < verses.Length; i++)
        {
            int randomIndex = Random.Range(0, verses.Length);
            string temp = verses[i];
            verses[i] = verses[randomIndex];
            verses[randomIndex] = temp;
        }
    }


    void CheckAnswer()
    {
        string[] words = verses[currentVerseIndex].Split(' ');
        Debug.Log(sentenceText.text);
        Debug.Log(verses[currentVerseIndex]);
        if (sentenceText.text.Trim() == verses[currentVerseIndex])
        {
            sentenceText.color = Color.green;
            currentVerseIndex++;
            if (currentVerseIndex < verses.Length)
            {
                StartCoroutine(NextVerseRoutine());
            }
            else
            {
                Debug.Log("All verses completed!");
            }
        }
        else
        {
            StartCoroutine(RepeatVerseRoutine());
        }
    }

    void ClearWords()
    {
        foreach (Transform child in panel)
        {
            Destroy(child.gameObject);
        }
    }

    void SpawnObjects()
    {

        string[] words = verses[currentVerseIndex].Split(' ');
        foreach (string word in words)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(-panel.rect.width / 2, panel.rect.width / 2),
                Random.Range(-panel.rect.height / 2, panel.rect.height / 2)
            );
            GameObject obj = Instantiate(wordPrefab, panel);
            obj.GetComponent<RectTransform>().anchoredPosition = randomPosition;
            TextMeshProUGUI textComponent = obj.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = word;
            }
        }
    }

    private IEnumerator NextVerseRoutine()
    {
        yield return new WaitForSeconds(1f);
        sentenceText.text = "";
        ClearWords();
        SpawnObjects();
        sentenceText.color = Color.black;
        sentenceText.text = "";
        verseBuilder.ResetSentence();
    }


    private IEnumerator RepeatVerseRoutine()
    {
        sentenceText.color = Color.red;
        yield return new WaitForSeconds(1f);
        ClearWords();
        SpawnObjects();
        sentenceText.text = "";
        sentenceText.color = Color.black;
        verseBuilder.ResetSentence();
    }





}
