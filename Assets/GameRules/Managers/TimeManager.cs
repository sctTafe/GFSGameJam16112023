using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

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


    public void ResetTime()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F;
    }

    public void SlowToEnd()
    {
        StartCoroutine(lerpOut(1f, .2f));
    }

    IEnumerator lerpOut(float totalLerpTime, float finalSpeed)
    {
        float currentSpeed = Time.timeScale;
        float lerpTime = 0f;
        
        while (lerpTime < totalLerpTime)
        {
            //Debug.Log(lerpTime/totalLerpTime);
            Time.timeScale = Mathf.Lerp(currentSpeed, finalSpeed, lerpTime / totalLerpTime);
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            lerpTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Ended slowmode");
    }
}