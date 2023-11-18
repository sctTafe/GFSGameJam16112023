using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLevelEnd : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.instance.ChangeToScene(nextSceneName);   
        }
    }
}
