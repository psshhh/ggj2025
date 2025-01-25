using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Topic
{
    STAR,
    COFFEE,
    FLOWER,
    GAME,
    FOOD
}

public class PersonController : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private LevelManager levelManager;
    private List<Group> people;

    private void Start()
    {
        levelManager = LevelManager.instance;
    }

    public void MovePerson(IPerson person, int previousTopic, int newTopic)
    {
        if (previousTopic != newTopic)
            levelManager.RemoveConversation(previousTopic, person.NumberOfPeople);
        
        levelManager.AddConversation(newTopic,person.NumberOfPeople);
        person.MovePerson(levelManager.GetConversationLocation(newTopic));

        //update the multiplier every time any person moves
        var count = levelManager.GetConversationCount(player.CurrentTopic);
        levelManager.SetPlayerMultiplier(count);
    }
}