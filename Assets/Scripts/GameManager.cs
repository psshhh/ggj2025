using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int levelNumber; //-1 for menu, 0 for tutorial
    private float levelDuration; //Get the LevelManager in the scene? and get levelDuration from there
    private float levelTimer; //Live countdown timer
    
    private LevelManager levelManager;
    
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
        
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    //puzzle mode
    public void PlayGame()
    {
        levelManager.StartLevel();
    }

    //arcade mode - get the highest score possible
    public void PlayArcade()
    {
        throw new Exception("Not yet implemented");
    }

    public void GetLevelDuration()
    {
        
    }
}
