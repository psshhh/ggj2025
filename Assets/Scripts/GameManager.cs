using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private GameObject nextLevelMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject playerInputButtons;
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button goButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button homeButton;
    
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
    }

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        goButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(StartGame);
        nextLevelButton.onClick.AddListener(StartGame);
        homeButton.onClick.AddListener(FullReset);

        //Just to speed things up
        if (mainMenu.activeSelf == false)
        {
            StartGame();
        }
    }

    //puzzle mode
    private void PlayGame()
    {
        instructions.SetActive(true);
        winMenu.SetActive(false);
    }

    private void StartGame()
    {
        if (mainMenu.activeSelf)
            mainMenu.SetActive(false);
        
        if (restartMenu.activeSelf)
            restartMenu.SetActive(false);
        
        if (nextLevelMenu.activeSelf)
            nextLevelMenu.SetActive(false);
        
        playerInputButtons.SetActive(true);
        LevelManager.instance.LoadLevel();
    }

    //arcade mode - get the highest score possible
    public void PlayArcade()
    {
        mainMenu.SetActive(false);
        throw new NotImplementedException();
    }

    public void ShowRestartLevel()
    {
        restartMenu.SetActive(true);
    }

    public void FullReset()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelManager.instance.FullReset();
        mainMenu.SetActive(true);
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
        playerInputButtons.SetActive(false);
        instructions.SetActive(false);
    }
    
    public void ShowNextLevelMenu()
    {
        nextLevelMenu.SetActive(true);
    }

    public void FadeInAndOut()
    {
        
    }
}
