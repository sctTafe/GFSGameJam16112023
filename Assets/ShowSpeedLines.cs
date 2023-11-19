using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.VFX;

public class ShowSpeedLines : MonoBehaviour
{
    public Rigidbody rb;
    private VisualEffect effect;
    //public float minSpeed = 10f;


    private void Start()
    {
        effect = GetComponent<VisualEffect>();
        effect.enabled = true;
    }

    void Update()
    {
        if (rb != null && effect != null)
        {
            effect.SetFloat("SpawnRate", rb.velocity.magnitude * 1);
        }
    }
}
