using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IPerson
{

    private int points;
    
    private void Awake()
    {
    }

    private void Start()
    {
        //Get level duration from GameManager
        // GameManager.instance.
        
        
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
        StartConversation(3, 10);
    }

    //Debug only
    private void StartConversation(int topicCount, float duration)
    {
        StartCoroutine(ConversationTimer(duration, topicCount));
    }
    
    public void StartConversation(int topicCount)
    {
        var duration = LevelManager.instance.LevelDuration;
        StartCoroutine(ConversationTimer(duration, topicCount));
    }
    
    IEnumerator ConversationTimer(float duration, int topicCount)
    {
        Debug.Log("conversation started");
        var conversationTime = duration / topicCount;
        
        for (var topicIndex = 0; topicIndex < topicCount; topicIndex++)
        {
            Debug.Log("started " + topicIndex);
            yield return new WaitForSeconds(conversationTime);
            Debug.Log("ended " + topicIndex);
            //Change the topic here currentTopic = etc 
        }
    }

    public void MovePerson()
    {
        throw new System.NotImplementedException();
    }
}
