using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLevelEnd : MonoBehaviour
{
    public string nextSceneName;
    public Canvas canvas;
    public LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.StopTimeAndShowButtons();   

            // Stop any ongoing coroutines to avoid conflicts
            //StopAllCoroutines();
            //
            //Start the coroutine to display the canvas for 5 seconds
            //StartCoroutine(DisplayCanvasForSeconds(5f));
        }
    }

    IEnumerator DisplayCanvasForSeconds(float seconds)
    {
        // Activate the canvas
        canvas.gameObject.SetActive(true);

        // Wait for the specified seconds
        yield return new WaitForSeconds(seconds);

        // Deactivate the canvas
        canvas.gameObject.SetActive(false);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
