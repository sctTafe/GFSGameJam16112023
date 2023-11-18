using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Stuff")]
    [SerializeField] private Canvas canvas;

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
        LevelManager.instance.HideCursor();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        GUIManager.instance.UpdateGameText(currentTime,score);
    }

    public void ResetPlayer(Vector3 pos, Vector3 rot)
    {
        player.fn_ZeroRigidBodyMomentum();
        player.transform.position = pos;
        player.transform.eulerAngles = rot;
    }
    public void ResetLevel()
    {
        LevelManager.instance.RestartLevel();
        score = 0;
        currentTime = 0f;
    }

    public void CollectPickup(int value)
    {
        score += value;
    }
}
