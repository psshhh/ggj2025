using UnityEngine;

public interface IPerson
{
    int CurrentTopic { get; }
    void StartConversation(int topicCount);
    void MovePerson(Vector3 destination);
}
