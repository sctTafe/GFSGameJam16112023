using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpannerLauncher : MonoBehaviour
{
    public GameObject _spannerPrefab;
    public float _launchForce = 10f;
    public float _arcHeight = 2f;

    void Update()
    {
        // Check for user input, for example, a mouse click
        if (Input.GetButtonDown("Fire1"))
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        // Create a new instance of the projectile prefab
        GameObject projectile = Instantiate(_spannerPrefab, transform.position, Quaternion.identity);

        // Calculate the direction from the camera to the mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 launchDirection = (transform.position - targetPosition).normalized;

        Debug.DrawRay(transform.position, launchDirection*100, Color.green, 5f);

        // Calculate the arc trajectory
        float distance = Vector3.Distance(transform.position, targetPosition);
        float yOffset = _arcHeight * distance / 10f; // Adjust the 10f factor to control the arc height

        // Apply force to launch the projectile in an arc
        Vector3 launchVelocity = launchDirection * _launchForce;
        launchVelocity.y += yOffset;

        // Apply the calculated velocity to the rigidbody of the projectile
        projectile.GetComponent<Rigidbody>().velocity = launchVelocity;
    }
}
