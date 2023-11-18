using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [Header("In-Game Buttons")]
    
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("UI Canvases")]
    
    [SerializeField] private GameObject GameUI;
    //[SerializeField] private GameObject PauseUI;
    //[SerializeField] private GameObject LevelEndUI;
    
    
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
    
    // Start is called before the first frame update

    public void UpdateGameText(float time, int score)
    {
        timeText.SetText(time.ToString("F2"));
        scoreText.SetText(score.ToString());
    }
    
    public void EndLevel()
    {
        GameUI.SetActive((false));
        //PauseUI.SetActive(false);
        //LevelEndUI.SetActive(true);
    }
}
