using UnityEngine;

public interface IPerson
{
    int CurrentTopic { get; }
    int NumberOfPeople { get; }
    void StartConversation();
    void MovePerson(Vector3 destination);
    void Reset();
}
