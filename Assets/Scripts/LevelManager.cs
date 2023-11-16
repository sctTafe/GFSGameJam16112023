using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextSceneName;

    private float startTime;
    public TextMeshProUGUI timeText; // Reference to your TextMeshPro component
    public GameObject uiButtonHolder; // Reference to your UI GameObject containing TextMeshPro and the button
    private bool timeStopped = false;
    public Controller player;

    void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        // If time is not stopped, calculate and display the elapsed time
        if (!timeStopped)
        {
            float elapsedTime = Time.time - startTime;
            string formattedTime = elapsedTime.ToString("F2");
            timeText.text = "Elapsed Time: " + formattedTime + " seconds";
        }
    }

    // Call this function to stop the time, move the text to the middle, and create the Next Level button
    public void StopTimeAndShowButtons()
    {
        timeStopped = true;


        // Move the TextMeshPro text to the middle of the screen
        Vector3 middleOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        timeText.rectTransform.position = middleOfScreen - new Vector3(timeText.rectTransform.sizeDelta.x / 2f, timeText.rectTransform.sizeDelta.y / 2f, 0f);

        // Enable the UI Buttons object
        uiButtonHolder.SetActive(true);

        ShowCursor();

        if (player != null) // need to stop the player but don't have a boolean for it in controller
        {
            //player.canMove = false;
        }
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
        SceneManager.LoadScene(nextSceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}