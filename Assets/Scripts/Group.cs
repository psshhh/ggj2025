using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Topic
// {
//     public string topicName;
//
//     public Topic(string topic)
//     {
//         topicName = topic;
//     }
// }

public class Group : MonoBehaviour, IPerson
{
    public int CurrentTopic => currentTopic;

    [SerializeField] private int numberOfPeople;
    [SerializeField] private List<Topic> topics;
    
    private int currentTopic = 0;
    
    private void Awake()
    {
        numberOfPeople = transform.childCount;
    }
    
    //for a group this is the groupNumber;
    public void StartConversation(int topicCount)
    {
        topicCount = topics.Count;
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
            if (topicIndex < topicCount - 1)
            {
                currentTopic = (int)topics[topicIndex++];
            }
        }
    }

    public void MovePerson(Vector3 destination)
    {
        transform.position = destination;
        //Play a cute math animation instead
    }

    // IEnumerator FloatBubble(Vector3 destination)
    // {
    //     
    // }
}
