using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GUIManager : MonoBehaviour
{
    [Header("In-Game Buttons")]
    
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("UI Canvases")]
    
    [SerializeField] private GameObject GameUI;
    //[SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject LevelEndUI;
    
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
        GameUI.SetActive(true);
        LevelEndUI.SetActive(false);
    }

    public void UpdateGameText(float time, int score)
    {
        timeText.SetText(time.ToString("F2"));
        scoreText.SetText(score.ToString());
    }
    
    public void DisplayEndLevelUI()
    {
        Debug.Log("End!");
        GameUI.SetActive((false));
        //PauseUI.SetActive(false);
        LevelEndUI.SetActive(true);
    }
}
