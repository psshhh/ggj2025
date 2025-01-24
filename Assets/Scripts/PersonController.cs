using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Topic
{
    STAR,
    COFFEE,
    FLOWER,
    LOVE,
    FRUIT
}

public class PersonController : MonoBehaviour
{
    private const int INIT_TOPIC = 1;
    
    [SerializeField] private Player player;
    
    private LevelManager levelManager;
    private List<Group> people;

    private void Start()
    {
        levelManager = LevelManager.instance;
    }

    public void InitMove()
    {
        //for all the people in the scene, group the like topics together
        //Player object & Group object

        foreach (IPerson person in people)
        {
            if (person.CurrentTopic > levelManager.ConversationLocations.Length)
                return;
            
            MovePerson(person, INIT_TOPIC, person.CurrentTopic);
        }
        //if group's topic == another group's topic, combine the groups together
        //if player's topic == group's topic
        //move player to group
        //then player can gain points x group's multiplier
        //if player's topic doesn't match any group, play the sad sound and sad face
    }

    private void ClearConversations()
    {

    }

    public void MovePerson(IPerson person, int previousTopic, int newTopic)
    {
        if (previousTopic >= 0)
            levelManager.ConversationLocations[previousTopic].RemoveCount();
        
        levelManager.ConversationLocations[newTopic].AddCount();
        person.MovePerson(levelManager.ConversationLocations[newTopic].transform.position);

        //update the multiplier every time any person moves
        var count = levelManager.ConversationLocations[player.CurrentTopic].GetCount();
        levelManager.SetPlayerMultiplier(count);
    }
}