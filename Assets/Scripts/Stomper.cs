using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    public bool disabled = false;
    public Animation stomp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StompLoop());
    }

    IEnumerator StompLoop()
    {
        while (true)
        {
            if (!disabled)
            {
                stomp.Play();
            }
            yield return new WaitForSeconds(stomp.clip.length + 0.1f);
        }
    }
}
