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
    }

    private void Start()
    {
    }
    // Call this function to show the mouse cursor

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