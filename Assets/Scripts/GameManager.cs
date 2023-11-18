using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [Header("UI Stuff")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private Controller player;

    private float currentTime = 0f;
    private int score = 0;
    
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        timeText.SetText("Time:" + currentTime.ToString("F2"));
        scoreText.SetText("Score: " + score);
    }

    public void PlayerDeath()
    {
        Reset();
    }
    void Reset()
    {
        currentTime = 0f;
        score = 0;
        player.fn_ZeroRigidBodyMomentum();
        player.gameObject.transform.position = spawnPoint.transform.position;
    }

    public void CollectPickup(int value)
    {
        score += value;
    }
}
