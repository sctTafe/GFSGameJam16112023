using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpeedLines : MonoBehaviour
{
    public Rigidbody rb;
    public float minSpeed = 10f;

    private ParticleSystem particles;
    public float maxOpacity = 1f;
    public float maxEmissionRate = 100f; // Placeholder value, replace with your desired rate

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (rb != null && particles != null)
        {
            if (rb.velocity.magnitude > minSpeed)
            {
                // Calculate the speed factor (normalized value above the threshold)
                float speedFactor = Mathf.Clamp01((rb.velocity.magnitude - minSpeed) / (2 * minSpeed));

                // Set particle system emission rate based on speed factor
                var emission = particles.emission;
                emission.rateOverTime = speedFactor * maxEmissionRate;

                // Set particle system opacity based on speed factor
                var mainModule = particles.main;
                Color particleColor = mainModule.startColor.color;
                particleColor.a = speedFactor * maxOpacity;
                mainModule.startColor = particleColor;
            }
            else
            {
                // If speed is below the threshold, stop emitting particles
                var emission = particles.emission;
                emission.rateOverTime = 0f;
            }
        }
    }
}
