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
        GUIManager.instance.HideCursor();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        
        ResetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        GUIManager.instance.UpdateGameText(currentTime, score);
    }

    public void ResetLevel()
    {
        //replace collectibles
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(true);
        }
        
        //replace player

        player.ResetVelocity();
        player.transform.position = spawnPoint.transform.position;
        player.transform.eulerAngles = spawnPoint.transform.eulerAngles;
        
        //reset run-specific values
        
        score = 0;
        currentTime = 0f;
        
        player.lockMovement(false);
        GUIManager.instance.HideCursor();
    }

    public void CompleteLevel(string nextSceneName)
    {
        player.lockMovement(true);
        GUIManager.instance.DisplayEndLevelUI();
        GUIManager.instance.ShowCursor();
    }

    public void CollectPickup(int value)
    {
        score += value;
    }
}
