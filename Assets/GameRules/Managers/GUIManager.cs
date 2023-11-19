using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GUIManager : MonoBehaviour
{
    [Header("In-Game")]
    
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("End-Level")] 
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private GameObject timePB;
    [SerializeField] private GameObject scorePB;

    [Header("Pre-Game")] 
    [SerializeField] private TextMeshProUGUI countdownText;
    
    [Header("UI Canvases")]
    
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject LevelEndUI;
    [SerializeField] private GameObject StartGameUI;
    
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Call this function to hide the mouse cursor
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public static GUIManager instance;

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
        ShowGameUI();
    }

    public void UpdateGameText(float time, int score)
    {
        timeText.SetText(time.ToString("F2"));
        scoreText.SetText(score.ToString());
    }

    public void ShowGameUI()
    {
        GameUI.SetActive(true);
        LevelEndUI.SetActive(false);
    }

    public void DisplayEndLevelUI(float time, int score, bool newTimePB, bool newScorePB)
    {
        GameUI.SetActive((false));
        //PauseUI.SetActive(false);
        LevelEndUI.SetActive(true);
        
        finalTimeText.SetText("Final Time: " + time.ToString("F2"));
        finalScoreText.SetText("Final Score: " + score.ToString());
        
        timePB.SetActive(newTimePB);
        scorePB.SetActive(newScorePB);
    }

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }
    
    IEnumerator Countdown()
    {
        StartGameUI.SetActive(true);
        countdownText.SetText("READY...");
        yield return new WaitForSeconds(2);
        for (int n = 3; n > 0; n--)
        {
            countdownText.SetText(n.ToString());
            yield return new WaitForSeconds(0.33f);
        }
        countdownText.SetText("RUN!");
        yield return new WaitForSeconds(1);
        StartGameUI.SetActive(false);
    }

}
