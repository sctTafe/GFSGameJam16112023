using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

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
        
        ShowCursor();
    }

    private void Start()
    {
    }
    // Call this function to show the mouse cursor
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

    public void ChangeToScene(string nextSceneName)
    {
        Debug.Log("Loading "+nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }

    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}