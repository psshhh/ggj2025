using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Controls the player's movement and decisions.
/// </summary>
public class Player : MonoBehaviour, IPerson
{
    public int CurrentTopic => currentTopic;
    public int NumberOfPeople => 1;

    private int currentTopic = 0;
    private int previousTopic = 0;
    private int points;

    //List of player input topics
    [SerializeField] private List<Topic> topics;
    [SerializeField] private List<Sprite> topicSprites;

    [SerializeField] private Button starBtn;
    [SerializeField] private Button coffeeBtn;
    [SerializeField] private Button flowerBtn;
    [SerializeField] private Button gameBtn;
    [SerializeField] private Button foodBtn;
    [SerializeField] private Button startLevelBtn;
    
    [SerializeField] private SpriteRenderer topicIcon;
    [SerializeField] private SpriteRenderer worriedIcon;
    [SerializeField] private TMPro.TMP_Text pointsText;
    
    private PersonController personController;
    private Vector3 originalPosition;
    private bool singleTopic = true;
    private Bubble bubble;

    private void Awake()
    {
        bubble = GetComponent<Bubble>();
        originalPosition = transform.position;
    }
    
    private void Start()
    {
        personController = LevelManager.instance.PersonController;
            
        starBtn.onClick.AddListener(()=> AddRemoveTopic(0));
        coffeeBtn.onClick.AddListener(()=> AddRemoveTopic(1));
        flowerBtn.onClick.AddListener(()=> AddRemoveTopic(2));
        gameBtn.onClick.AddListener(()=> AddRemoveTopic(3));
        foodBtn.onClick.AddListener(()=> AddRemoveTopic(4));
        
        startLevelBtn.onClick.AddListener(StartConversation);
    }

    private void Update()
    {
        if (pointsText != null)
            pointsText.text = LevelManager.instance.PlayerPoints + "00";
    }

    public void StartConversation()
    {
        ToggleInputButtons(false);
        LevelManager.instance.StartLevel();
        var duration = LevelManager.instance.LevelDuration;
        currentTopic = (int)topics[0];
        previousTopic = currentTopic;
        StartCoroutine(ConversationTimer(duration, topics.Count));
    }
    
    IEnumerator ConversationTimer(float duration, int topicCount)
    {
        Debug.Log("conversation started with " + topicCount + " topics");
        var conversationTime = duration / topicCount;
        
        for (var topicIndex = 0; topicIndex < topicCount; topicIndex++)
        {
            currentTopic = (int)topics[topicIndex];
            
            var topic = (Topic)currentTopic;
            Debug.Log("started talking about " + topic);
            personController.MovePerson(this, previousTopic, currentTopic);
            yield return new WaitForSeconds(conversationTime);
            Debug.Log("ended talking about " + topic);
            previousTopic = currentTopic;
        }

        Debug.Log("finished conversations");
    }

    public void MovePerson(Vector3 destination)
    {
        bubble.UpdateBubble(destination);
        transform.position = destination;
        //Play a cute math animation instead
    }

    public void MakeSad(bool active)
    {
        worriedIcon.enabled = active;
    }
    private void AddRemoveTopic(int topic)
    {
        var topicToChange = (Topic)topic;
        
        if (topics.Contains(topicToChange) == false)
        {
            if (singleTopic)
            {
                topics.Clear();
            }

            topics.Add(topicToChange);
            topicIcon.sprite = topicSprites[topic];
            startLevelBtn.enabled = true;
        }
        else
        {
            topics.Remove(topicToChange);
            topicIcon.sprite = topicSprites[5];
            startLevelBtn.enabled = false;
        }
    }

    public void Reset()
    {
        transform.position = originalPosition;
        topics.Clear();
        topicIcon.sprite = null;
        ToggleInputButtons(true);
        startLevelBtn.enabled = false;
        MakeSad(false);
        bubble.ResetBubble();
    }

    private void ToggleInputButtons(bool active)
    {
        starBtn.enabled = active;
        coffeeBtn.enabled = active;
        flowerBtn.enabled = active;
        gameBtn.enabled = active;
        foodBtn.enabled = active;
        startLevelBtn.enabled = active;
    }
}
