using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNotY : MonoBehaviour
{
    public float yOffset = 0f;
    public Transform toFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(toFollow.position.x, yOffset, toFollow.position.z);
    }
}
