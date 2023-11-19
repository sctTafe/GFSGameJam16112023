using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    public float waitTime = 0f;
    public float offset = 0f;
    public bool disabled = false;
    public Animation stomp;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StompLoop());
    }

    IEnumerator StompLoop()
    {
        yield return new WaitForSeconds(offset);
        while (true)
        {
            if (!disabled)
            {
                stomp.Play();
            }
            yield return new WaitForSeconds(stomp.clip.length + waitTime +0.1f);
        }
    }

    public void fn_ToggleStomperOn()
    {
        disabled = !disabled;
    }
}
