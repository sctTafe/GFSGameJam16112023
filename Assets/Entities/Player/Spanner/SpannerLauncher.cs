using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpannerLauncher : MonoBehaviour
{
    public Transform _cammeraTransform;
    public Vector3 _launcherOffset;
    public GameObject _spannerPrefab;
    public float _launchForce = 10f;
    public float _arcHeight = 2f;


    private void Start()
    {
        _cammeraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Check for user input, for example, a mouse click
        if (Input.GetButtonDown("Fire1"))
        {
            LaunchProjectile();
        }
        //transform.position = _cammeraTransform.position;
    }

    void LaunchProjectile()
    {
        // Create a new instance of the projectile prefab
        GameObject projectile = Instantiate(_spannerPrefab, transform.position, _cammeraTransform.rotation);

        // Apply the calculated velocity to the rigidbody of the projectile
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward*_launchForce;
    }
}
