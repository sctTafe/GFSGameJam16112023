using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public UnityEvent _OnCompletion;
    public string _nextScene;

    public void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void fn_EndLevel()
    {
        _OnCompletion?.Invoke();
        StartCoroutine(SwitchLevel(3f));
    }

    IEnumerator SwitchLevel(float time)
    {
        yield return new WaitForSeconds(time);
        SwitchToScene(_nextScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fn_EndLevel();
            Debug.Log("Player Reached Level End!");
        }
    }
}
