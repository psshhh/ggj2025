using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    // public int number;
    public float duration;
    public float pointTarget;
    public GameObject levelObjects;

    public List<Group> groups;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    [SerializeField] private Level[] levels;
    [SerializeField] private ConversationLocation[] locations;
    [SerializeField] private Player player;
    
    private PersonController personController;
    private int currentLevel;
    private float levelDuration;
    private float levelTime;
    
    private float pointTime;
    private int delayAmount = 1; //the seconds count
    private bool started = false;
    private bool finished = false;
    private int playerMultiplier;
    private int playerPoints;

    public float CurrentLevelTarget
    {
        get { return levels[currentLevel].pointTarget; }
    }

    public float LevelDuration
    {
        get { return levels[currentLevel].duration; }
    }

    public PersonController PersonController
    {
        get { return personController; }
    }

    public ConversationLocation[] ConversationLocations
    {
        get { return locations; }
    }

    public int PlayerPoints => playerPoints;
    
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } 
        else if (instance != this)
        {
            Destroy(this.gameObject);
        } 
        
        personController = GetComponent<PersonController>();
    }

    private void Start()
    {
        foreach (Level level in levels)
        {
            foreach (Group group in level.levelObjects.GetComponentsInChildren<Group>())
            {
                level.groups.Add(group);
            }
        }
    }

    private void Update()
    {
        if (started)
        {
            if (levelTime > 0)
            {
                levelTime -= Time.deltaTime;
                
                pointTime += Time.deltaTime;
                if (playerMultiplier > 1)
                {
                    if (pointTime > delayAmount)
                    {
                        pointTime = 0;
                        playerPoints += 1 * playerMultiplier;
                        player.MakeSad(false);
                    }
                }
                else
                {
                    player.MakeSad(true);
                }
            }
            else
            {
                if (playerPoints < CurrentLevelTarget)
                {
                    //Show the failure menu, and an option to restart
                    GameManager.instance.ShowRestartLevel();
                    //wait... for the player to hit the restart button which just plays StartLevel()
                }
                else
                {
                    EndLevel();
                }
            }
        }
    }

    public void LoadLevel()
    {
        //do a blank screen cover
        
        levels[currentLevel].levelObjects.SetActive(true);
        ResetLevel();
        //The player's input buttons setactive, there's a play button here that starts the level
    }
        
    [ContextMenu("Start Level")]
    public void StartLevel()
    {
        Debug.Log("Starting Level");
        levelTime = levels[currentLevel].duration;
        started = true;
        foreach (Group group in levels[currentLevel].groups)
        {
            group.StartConversation();
        }
        Debug.Log("Started Level");
    }

    [ContextMenu("Stop Level")]
    public void EndLevel()
    {
        Debug.Log("Finished Level");
        if (currentLevel < levels.Length - 1)
        {
            levels[currentLevel].levelObjects.SetActive(false);
            started = false;
            currentLevel++; 
            //queue up next level
            // LoadLevel(); //Opted to do player input to take a break
            GameManager.instance.ShowNextLevelMenu();
        }
        else
        {
            Debug.Log("Finished GAME");
            //display the you win screen!
            //more levels coming soon!
            GameManager.instance.ShowWinMenu();
            started = false;
        }
    }

    private void ResetLevel()
    {
        Debug.Log("Resetting Level");
        player.Reset();
        foreach (Group group in levels[currentLevel].groups)
        {
            group.Reset();
        }
        ClearConversation();
        levelTime = levels[currentLevel].duration;
        pointTime = 0;
        playerPoints = 0;
        started = false;
    }

    public void FullReset()
    {
        levels[currentLevel].levelObjects.SetActive(false);
        currentLevel = 0;
    }
    
    public void SetPlayerMultiplier(int multiplier)
    {
        if (multiplier <= 0)
            playerMultiplier = 1;
        playerMultiplier = multiplier;
    }

    public void RemoveConversation(int topic, int multiplier)
    {
        ConversationLocations[topic].RemoveCount(multiplier);
    }
    
    public void AddConversation(int topic, int multiplier)
    {
        ConversationLocations[topic].AddCount(multiplier);
    }

    public Vector3 GetConversationLocation(int topic)
    {
        return ConversationLocations[topic].transform.position;
    }

    public int GetConversationCount(int topic)
    {
        return ConversationLocations[topic].GetCount();
    }

    private void ClearConversation()
    {
        foreach (ConversationLocation conversation in ConversationLocations)
        {
            conversation.ClearCount();
        }
    }
}