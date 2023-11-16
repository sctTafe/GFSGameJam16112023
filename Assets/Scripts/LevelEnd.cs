using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public GameObject endGameUI;
    // Start is called before the first frame update

    void Start()
    {
        endGameUI.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        Debug.Log("you win");
        endGameUI.SetActive(true);
    }
}
