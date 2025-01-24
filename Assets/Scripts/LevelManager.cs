using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public int number;
    public float duration;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    [SerializeField] private int levelNumber; //-1 for menu, 0 for tutorial
    [SerializeField] private float levelDuration;
    [SerializeField] private Level[] levels;
    [SerializeField] private ConversationLocation[] locations;
    
    private PersonController personController;
    private float levelTime;
    private float pointTime;
    private int delayAmount = 1; //the seconds count
    private bool started = false;
    private bool finished = false;
    private int playerMultiplier;
    private float playerPoints;

    public int LevelNumber
    {
        get { return levelNumber; }
    }

    public float LevelDuration
    {
        get { return levelDuration; }
    }

    public float LevelTime
    {
        get { return levelTime; }
    }

    public ConversationLocation[] ConversationLocations
    {
        get { return locations; }
    }
    
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
                    if (pointTime >= delayAmount)
                    {
                        playerPoints += 1 * playerMultiplier;
                    }
                }
            }
            else
            {
                if (finished == false)
                {
                    //Show the failure menu, and an option to restart
                    //RestartLevel(); //This for real
                    started = false; //temp for debugging
                    return;
                }
                EndLevel();
            }
        }
    }
        
    [ContextMenu("Start Level")]
    public void StartLevel()
    {
        Debug.Log("Starting Level");
        ResetLevel();
        started = true;
        Debug.Log("Started Level");
    }

    [ContextMenu("Stop Level")]
    public void EndLevel()
    {
        if (finished)
        {
            Debug.Log("Finished Level");
            levelNumber++; //This makes it seem like you need a Level Class so you can
                           //increment through a level list and not have multiple scenes
                           //because I hate that anyway
            ResetLevel();
            //queue up next level
        }
        //temporarily stop the level
        started = false;
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting Level");
        //queue up restart
        
        ResetLevel();
        //wait... or maybe make this a player input restart
        StartLevel();
    }

    private void ResetLevel()
    {
        Debug.Log("Resetting Level");
        levelTime = levelDuration;
        started = false;
    }

    public void SetPlayerMultiplier(int multiplier)
    {
        if (multiplier <= 0)
            playerMultiplier = 1;
        playerMultiplier = multiplier;
    }
}