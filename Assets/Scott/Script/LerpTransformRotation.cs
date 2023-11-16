using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTransformRotation : MonoBehaviour
{
    
    public Vector3 _targetRotation = new Vector3(0f, 30f, 0f); // Target Euler angles
    public float rotationTime = 2f; // Time taken to complete the rotation
    private float elapsedTime = 0f;
    private Vector3 startingRotationEuler;

    bool lerpTo = false;
    bool lerpFrom = false;
    bool learpFinished = false;

    bool isUp = false;

    private void Start()
    {
        startingRotationEuler = transform.localEulerAngles;
    }

    void Update()
    {
        if (learpFinished == false && lerpTo == true)
        {
            LerpToTarget();
        }

        if (learpFinished == false && lerpFrom == true)
        {
            LerpToStartRotation();
        }
    }

    public void fn_LerpTo()
    {
        if (lerpTo != true)
        {
            learpFinished = false;
            elapsedTime = 0f;
            lerpTo = true;
            lerpFrom = false;
        }
    }
    public void fn_LerpBack()
    {
        if (lerpFrom != true)
        {
            learpFinished = false;
            elapsedTime = 0f;
            lerpFrom = true;
            lerpTo = false;

        }

    }

    void LerpToTarget()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;
        // Calculate the interpolation factor (t) based on elapsed time and total rotation time
        float t = Mathf.Clamp01(elapsedTime / rotationTime);
        // Use Quaternion.Lerp to smoothly interpolate between the current rotation and the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(startingRotationEuler + _targetRotation), t);
        // If the interpolation is complete, reset the elapsed time
        if (t >= 1f)
        {
            elapsedTime = 0f;
            learpFinished = true;
            isUp = true;
        }
    }
    void LerpToStartRotation()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;
        // Calculate the interpolation factor (t) based on elapsed time and total rotation time
        float t = Mathf.Clamp01(elapsedTime / rotationTime);
        // Use Quaternion.Lerp to smoothly interpolate between the current rotation and the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(startingRotationEuler), t);
        // If the interpolation is complete, reset the elapsed time
        if (t >= 1f)
        {
            elapsedTime = 0f;
            learpFinished = true;
            isUp = false;
        }
    }

    public void fn_toggleRotation()
    {
        if (isUp)
        {
            fn_LerpBack();
        }
        else
        {
            fn_LerpTo();
        }
    }


}
