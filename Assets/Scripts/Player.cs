using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPerson
{
    public int CurrentTopic => currentTopic;

    private int currentTopic = 0;
    private int previousTopic = 0;
    private int points;

    //List of player input topics
    [SerializeField] private List<Topic> topics;
    
    private PersonController personController;
    
    private void Awake()
    {
    }

    private void Start()
    {
        //Get level duration from GameManager
        // GameManager.instance.
        
        personController = FindFirstObjectByType<PersonController>();
    }

    private void Update()
    {
        //if the game has started
        //and the player has "topics" to say
        //they confirm their topics (it's probably going to be a button
        //Get multiplier from playerinput (how many topics they have)
        //Start the conversation queue
        //foreach topic they have 
        //StartConversation
        //Change topic
        
    }

    [ContextMenu("Start Convo")]
    public void StartConvo()
    {
        StartConversation(topics.Count);
    }

    //Debug only
    private void StartConversation(int topicCount, float duration)
    {
        StartCoroutine(ConversationTimer(duration, topicCount));
    }

    public void StartConversation(int topicCount)
    {
        var duration = LevelManager.instance.LevelDuration;
        currentTopic = (int)topics[0];
        previousTopic = currentTopic;
        StartCoroutine(ConversationTimer(duration, topicCount));
    }
    
    IEnumerator ConversationTimer(float duration, int topicCount)
    {
        Debug.Log("conversation started with " + topicCount + " topics");
        var conversationTime = duration / topicCount;
        
        for (var topicIndex = 0; topicIndex < topicCount; topicIndex++)
        {
            currentTopic = (int)topics[topicIndex];
            
            var topic = topics[currentTopic];
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
        transform.position = destination;
        //Play a cute math animation instead
    }
}
