using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VerseManager : MonoBehaviour
{
    public GameObject wordPrefab;
    public RectTransform panel;
    public TextMeshProUGUI sentenceText;

    public TextMeshProUGUI categoryText;
    public Button nextButton;

     public Button backButton;

    public VerseBuilder verseBuilder;

    // TODO: toggle answer text on mouse down
    public TextMeshProUGUI answerText;
    public bool showAnswerText;

    // Define your categories with related verses
   private Dictionary<string, string[]> categoryVerses = new Dictionary<string, string[]>
{
    {
        "Anxiety", new string[]
        {
            "Cast all your anxiety on Him because He cares for you",
            "Do not be anxious about anything, but in every situation, by prayer and petition, with thanksgiving, present your requests to God",
            "The peace of God, which transcends all understanding, will guard your hearts",
            "When anxiety was great within me, your consolation brought me joy",
            "Do not worry about tomorrow, for tomorrow will worry about itself",
            "I sought the Lord, and He answered me; He delivered me from all my fears",
            "When I am afraid, I put my trust in you",
            "The Lord is near to the brokenhearted and saves the crushed in spirit",
            "He will keep in perfect peace those whose minds are steadfast, because they trust in you",
            "Give all your worries and cares to God, for He cares about you",
            "Fear not, for I am with you; be not dismayed, for I am your God",
            "The Lord is my light and my salvation; whom shall I fear?",
            "When I am afraid, I put my trust in you",
            "Peace I leave with you; my peace I give you",
            "The Lord will fight for you; you need only to be still"
        }
    },
    {
        "Praise", new string[]
        {
            "Let everything that has breath praise the Lord",
            "I will bless the Lord at all times; His praise shall continually be in my mouth",
            "Praise the Lord, my soul, and forget not all his benefits",
            "I will give thanks to you, Lord, with all my heart",
            "Great is the Lord and most worthy of praise",
            "Sing to the Lord a new song; sing to the Lord, all the earth",
            "Praise be to the Lord, to God our Savior, who daily bears our burdens",
            "Let the peoples praise you, O God; let all the peoples praise you",
            "The Lord is near to all who call on him, to all who call on him in truth",
            "Praise the Lord, all you His servants who minister by night in the house of the Lord",
            "I will extol the Lord at all times; His praise will always be on my lips",
            "Oh, give thanks to the Lord, for He is good; for His steadfast love endures forever",
            "Let the heavens rejoice, let the earth be glad; let them say among the nations, 'The Lord reigns!'",
            "Great is the Lord and most worthy of praise; His greatness no one can fathom",
            "I will bless the Lord at all times; His praise shall continually be in my mouth"
        }
    },
    {
        "Faith", new string[]
        {
            "Now faith is confidence in what we hope for and assurance about what we do not see",
            "For we live by faith, not by sight",
            "If you have faith as small as a mustard seed, you can say to this mountain, 'Move from here to there,' and it will move",
            "And without faith, it is impossible to please God",
            "Faith is being sure of what we hope for and certain of what we do not see",
            "For we walk by faith, not by sight",
            "Therefore, since we have been justified through faith, we have peace with God through our Lord Jesus Christ",
            "If you believe, you will receive whatever you ask for in prayer",
            "For we live by faith and not by sight",
            "Take delight in the Lord, and He will give you the desires of your heart",
            "So faith comes from hearing, and hearing through the word of Christ",
            "The righteous shall live by faith",
            "But the one who doubts is like a wave of the sea, blown and tossed by the wind",
            "And Jesus answered them, 'Have faith in God'",
            "And Jesus said to him, 'If you can! All things are possible for one who believes'"
        }
    },
    {
        "Righteousness", new string[]
        {
            "Blessed are those who hunger and thirst for righteousness, for they will be filled",
            "For the righteous will never be moved; they will be remembered forever",
            "The Lord loves the righteous and will not forsake them",
            "The prayer of the righteous is powerful and effective",
            "The righteous person may have many troubles, but the Lord delivers him from them all",
            "But the righteousness that is by faith says: 'Do not say in your heart, 'Who will ascend into heaven?' (that is, to bring Christ down)'",
            "Righteousness guards the person of integrity, but wickedness overthrows the sinner",
            "For the righteous will inherit the land and dwell in it forever",
            "The eyes of the Lord are on the righteous, and His ears are attentive to their cry",
            "The Lord is righteous in all His ways and faithful in all He does",
            "The way of the righteous is level; you make level the way of the righteous",
            "I will rejoice in the Lord, I will be glad in His salvation",
            "He has made everything beautiful in its time",
            "The wicked flee when no one pursues, but the righteous are as bold as a lion",
            "Righteousness exalts a nation, but sin is a disgrace to any people"
        }
    }
};

    private string[] currentCategoryVerses;
    private int currentVerseIndex = 0;

    void Start()
    {
        // Get category from PlayerPrefs and load the relevant verses
        string category = PlayerPrefs.GetString("Category", "Anxiety"); // Default category is "Anxiety"
        categoryText.text = category;
        LoadCategoryVerses(category);
        ShuffleVerses();
        nextButton.onClick.AddListener(CheckAnswer);
        backButton.onClick.AddListener(BackToHome);
        SpawnObjects();
    }

    void BackToHome(){
          SceneManager.LoadScene("Menu");
    }

    void LoadCategoryVerses(string category)
    {
        if (categoryVerses.ContainsKey(category))
        {
            currentCategoryVerses = categoryVerses[category];
        }
        else
        {
            currentCategoryVerses = categoryVerses["Anxiety"]; // Default fallback
        }
    }

    void ShuffleVerses()
    {
        for (int i = 0; i < currentCategoryVerses.Length; i++)
        {
            int randomIndex = Random.Range(0, currentCategoryVerses.Length);
            string temp = currentCategoryVerses[i];
            currentCategoryVerses[i] = currentCategoryVerses[randomIndex];
            currentCategoryVerses[randomIndex] = temp;
        }
    }

    void CheckAnswer()
    {
        string[] words = currentCategoryVerses[currentVerseIndex].Split(' ');
        Debug.Log(sentenceText.text);
        Debug.Log(currentCategoryVerses[currentVerseIndex]);
        if (sentenceText.text.Trim() == currentCategoryVerses[currentVerseIndex])
        {
            sentenceText.color = Color.green;
            currentVerseIndex++;
            if (currentVerseIndex < currentCategoryVerses.Length)
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
        answerText.text =currentCategoryVerses[currentVerseIndex];
        string[] words = currentCategoryVerses[currentVerseIndex].Split(' ');

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
