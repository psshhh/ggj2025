using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the groups of people having conversations.
///  
/// Almost a duplicate of Player code but ran out of time to convert IPerson into a
/// Person Monobehaviour that would have consolidated these scripts
/// </summary>
public class Group : MonoBehaviour, IPerson
{
    public int CurrentTopic => currentTopic;
    public Bubble Bubble => bubble;

    [SerializeField] private int numberOfPeople;
    [SerializeField] private List<Topic> topics;
    
    private PersonController personController;
    private Vector3 originalPosition;
    private int currentTopic = 0;
    private int previousTopic = 0;

    private Bubble bubble;
    private Vector3 currentPosition;

    public int NumberOfPeople => numberOfPeople;
    
    private void Awake()
    {
        numberOfPeople = topics.Count;
        originalPosition = transform.position;
        personController = LevelManager.instance.PersonController;
        bubble = GetComponent<Bubble>();
    }

    public void StartConversation()
    {
        var duration = LevelManager.instance.LevelDuration;
        currentTopic = (int)topics[0];
        previousTopic = currentTopic;
        StartCoroutine(ConversationTimer(duration, topics.Count));
    }
    
    IEnumerator ConversationTimer(float duration, int topicCount)
    {
        var conversationTime = duration / topicCount;
        
        for (var topicIndex = 0; topicIndex < topicCount; topicIndex++)
        {
            currentTopic = (int)topics[topicIndex];
            
            personController.MovePerson(this, previousTopic, currentTopic);
            yield return new WaitForSeconds(conversationTime);
            previousTopic = currentTopic;
        }
    }

    public void MovePerson(Vector3 destination)
    {
        var randomX = Random.Range(-1f, 1f);
        var randomY = Random.Range(-1f, 1f);
        var offset = new Vector3(destination.x + randomX, destination.y + randomY, destination.z);
        bubble.MovePausedBubble();
        
        StartCoroutine(LerpTo(offset));
    }
    
    IEnumerator LerpTo(Vector3 destination)
    {
        var elapsedTime = 0f;
        var waitTime = 1;
        currentPosition = transform.position;
        
        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(currentPosition, destination, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }  
        
        transform.position = destination;
        bubble.UpdateBubble(destination);
        yield return null;
    }
    
    public void Reset()
    {
        transform.position = originalPosition;
        bubble.ResetBubble();
    }
}
