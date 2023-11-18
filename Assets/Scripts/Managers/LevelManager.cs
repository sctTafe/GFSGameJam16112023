using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    
    private GameObject[] collectibles;
    
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
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
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

    public void RestartLevel()
    {
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(true);
        }

        GameManager.instance.ResetPlayer(spawnPoint.transform.position, spawnPoint.transform.eulerAngles);
    }

    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}