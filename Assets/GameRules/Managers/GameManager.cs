using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Stuff")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject spawnPoint;

    private PlayerController player;

    private float currentTime = 0f;
    private int score = 0;
    
    private GameObject[] collectibles;

    private float bestTime = 999f;
    private int bestScore = 0;
    
    public enum GameState
    {
        starting, 
        active,
        finished
    }

    private GameState state;
    
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        //find needed gameObjects 
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        
        //set up
        
        GUIManager.instance.HideCursor();
        ResetLevel();
        
        state = GameState.starting;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (state == GameState.active)
        {
            currentTime += Time.deltaTime;

            GUIManager.instance.UpdateGameText(currentTime, score);
        }
    }
    
    public void CollectPickup(int value)
    {
        score += value;
    }
    
    public void CompleteLevel(string nextSceneName)
    {
        state = GameState.finished;
        player.lockMovement(true);
        player.lockAiming(true);
        GUIManager.instance.DisplayEndLevelUI(currentTime, score, currentTime < bestTime, score > bestScore);
        GUIManager.instance.ShowCursor();
        TimeManager.instance.SlowToEnd();

        if (score > bestScore)
        {
            bestScore = score;
        }

        if (currentTime < bestTime)
        {
            bestTime = currentTime;
        }
    }

    public void ResetLevel()
    {
        StartCoroutine(StartLevel());
    }


    IEnumerator StartLevel()
    {
        state = GameState.starting;
        
        player.lockAiming(false);
        player.lockMovement(true);
        
        //replace player

        player.ResetVelocity();
        player.transform.position = spawnPoint.transform.position;
        player.transform.eulerAngles = spawnPoint.transform.eulerAngles;

        GUIManager.instance.ShowGameUI();
        GUIManager.instance.HideCursor();
        TimeManager.instance.ResetTime();
        
        score = 0;
        currentTime = 0f;
        
        GUIManager.instance.UpdateGameText(0f, 0);
        
        GUIManager.instance.StartCountdown();
        
        //replace collectibles
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(true);
        }
        
        //reset run-specific values
        
        
        yield return new WaitForSeconds(3f);
        
        state = GameState.active;
        
        player.lockMovement(false);
    }
}
