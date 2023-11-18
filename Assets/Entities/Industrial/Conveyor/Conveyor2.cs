using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor2 : MonoBehaviour
{
    public float conveyorSpeed = 5f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply a continuous force in the conveyor belt direction
            rb.AddForce(transform.forward * conveyorSpeed, ForceMode.VelocityChange);
        }
    }
}
