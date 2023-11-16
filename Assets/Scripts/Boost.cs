using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public bool canBoost = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (canBoost)
        {
            StartCoroutine(bounceCD());
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 11f, rb.velocity.z) * 1.25f;
        }
    }

    IEnumerator bounceCD()
    {
        canBoost = false;
        yield return new WaitForSeconds(0.5f);
        canBoost = true;
    }
}
