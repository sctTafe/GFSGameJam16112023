using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = rb.position;
        rb.position += Vector3.back * speed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }
}
