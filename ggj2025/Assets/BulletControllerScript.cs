using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BulletControllerScript : MonoBehaviour
{
    public GameObject bulletPrefab;        // Prefab for the bullet
    public Transform firePoint;            // Point where the bullet is spawned
    public float bulletSpeed = 20f;        // Speed of the bullet
    public Color bulletLightColor = Color.white; // Color of the light attached to the bullet
    public float lightIntensity = 5f;      // Intensity of the light

    private void Update( )
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void ShootBullet()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z position is 0 for 2D

        // Calculate the direction from the firePoint to the mouse position
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Calculate the angle of the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instantiate the bullet and set its position and rotation based on the angle
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));

        // Set the bullet's velocity to move in the direction of the mouse
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        // Add a 2D Light to the bullet
        Light2D light = bullet.AddComponent<Light2D>();
        light.lightType = Light2D.LightType.Point; // Point light
        light.color = bulletLightColor;           // Set light color
        light.intensity = lightIntensity;         // Set light intensity
        light.pointLightOuterRadius = 2f;         // Set light radius

    }
}
