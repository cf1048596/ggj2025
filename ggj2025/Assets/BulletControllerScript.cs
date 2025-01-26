using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BulletControllerScript : MonoBehaviour
{
    public GameObject bulletPrefab;        // Prefab for the bullet
    public Transform firePoint;            // Point where the bullet is spawned
    public float bulletSpeed = 20f;        // Speed of the bullet
    public Color bulletLightColor = Color.white; // Color of the light attached to the bullet
    public float lightIntensity = 5f;      // Intensity of the light
    public GameObject playerObject;
    private int shots = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerObject.GetComponent<PlayerScript>()!=null)
            {
                if (shots >= 5)
                {
                    playerObject.GetComponent<PlayerScript>().doubletapvar = false;
                    shots = 0;
                } else if (playerObject.GetComponent<PlayerScript>().doubletapvar == true)
                {
                    ShootTwoBullets();
                    shots++;
                } else if (shots <= 0)
                {
                    ShootBullet();
                }

            }
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
    private void ShootTwoBullets()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z position is 0 for 2D

        // Calculate the direction from the firePoint to the mouse position
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Calculate the base angle of the direction vector
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Fire the first bullet 22.5 degrees to the right of the base angle
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0, 0, baseAngle + 22.5f)));
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        if (rb1 != null)
        {
            Vector2 direction1 = new Vector2(Mathf.Cos((baseAngle + 22.5f) * Mathf.Deg2Rad), Mathf.Sin((baseAngle + 22.5f) * Mathf.Deg2Rad));
            rb1.linearVelocity = direction1 * bulletSpeed;
        }

        // Add light to the first bullet
        Light2D light1 = bullet1.AddComponent<Light2D>();
        light1.lightType = Light2D.LightType.Point;
        light1.color = bulletLightColor;
        light1.intensity = lightIntensity;
        light1.pointLightOuterRadius = 2f;

        // Fire the second bullet 22.5 degrees to the left of the base angle
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0, 0, baseAngle - 22.5f)));
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        if (rb2 != null)
        {
            Vector2 direction2 = new Vector2(Mathf.Cos((baseAngle - 22.5f) * Mathf.Deg2Rad), Mathf.Sin((baseAngle - 22.5f) * Mathf.Deg2Rad));
            rb2.linearVelocity = direction2 * bulletSpeed;
        }

        // Add light to the second bullet
        Light2D light2 = bullet2.AddComponent<Light2D>();
        light2.lightType = Light2D.LightType.Point;
        light2.color = bulletLightColor;
        light2.intensity = lightIntensity;
        light2.pointLightOuterRadius = 2f;
    }

}
